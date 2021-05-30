using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProcessPage : ContentPage
    {
        private string passwordHash;
        private string login;

        public ProcessPage(string login, string passwordHash)
        {
            InitializeComponent();

            this.login = login;
            this.passwordHash = passwordHash;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var obj = await Authorization.SignIn(login, passwordHash);

                //Если такой пользователь не был найден в базе данных - выводится сообщение об ошибке.
                if (obj["status"].ToString() == "fail")
                {
                    Android.Widget.Toast.MakeText(App.Context, obj["message"].ToString(), Android.Widget.ToastLength.Long).Show();
                    return;
                }

                if (obj["status"].ToString() == "success")
                {
                    User.GetUser(JsonConvert.DeserializeObject<User>(obj["result"].ToString()));
                    App.Current.Properties["auth_token"] = obj["result"]["auth_token"].ToString();
                }

                //await Shell.Current.GoToAsync($"//Home", true);
            }
            catch (Java.Net.SocketTimeoutException)
            {
                Android.Widget.Toast.MakeText(App.Context, "Ошибка подключения к серверу", Android.Widget.ToastLength.Long).Show();
            }
            finally
            {
                await Shell.Current.GoToAsync($"//Account", true);
            }
        }
    }
}