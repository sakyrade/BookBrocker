using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentView
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public SignInView()
        {
            InitializeComponent();
        }

        private async void LogIn(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(App.Context, "Отсутствует подключение к Интернету", ToastLength.Long).Show();
                return;
            }

            //Проверка введённых в Entry данных.
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Ошибка", "Не введены логин или пароль", "OK");
                return;
            }

            //Генерация хэш-кода с помощью хэш-функции.
            string passwordHash = Crypto.CreateHashCode(Password);

            await Navigation.PushModalAsync(new ProcessPage(Login, passwordHash));
        }

        private async void Registration(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(App.Context, "Отсутствует подключение к Интернету", ToastLength.Long).Show();
                return;
            }

            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}