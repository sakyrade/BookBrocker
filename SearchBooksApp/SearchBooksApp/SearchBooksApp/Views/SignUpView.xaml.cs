using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationView : ContentView
    {
        private string nickname;
        private string password;
        private string passwordRepeat;
        private string sex;
        private int age;

        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged("Nickaname");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public string PasswordRepeat
        {
            get { return passwordRepeat; }
            set
            {
                passwordRepeat = value;
                OnPropertyChanged("PasswordRepeat");
            }
        }

        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public RegistrationView()
        {
            InitializeComponent();
        }

        private void EntryNickname_Unfocused(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Nickname)) return;

            if (Nickname.Length < 2)
            {
                Toast.MakeText(App.Context, "Недопустимый никнейм", ToastLength.Long).Show();
                Nickname = null;
            }
        }

        private void EntryPassword_Unfocused(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Password)) return;

            if (Password.Length < 6)
            {
                Toast.MakeText(App.Context, "Пароль должен быть больше 6 символов", ToastLength.Long).Show();
                Password = null;
                return;
            }
        }

        private void EntryPasswordRepeat_Unfocused(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordRepeat)) return;

            if (string.IsNullOrEmpty(Password))
            {
                Toast.MakeText(App.Context, "Вы не ввели пароль", ToastLength.Long).Show();
                PasswordRepeat = null;
                return;
            }

            if (Password != PasswordRepeat)
            {
                Toast.MakeText(App.Context, "Пароли не совпадают", ToastLength.Long).Show();
                PasswordRepeat = null;
            }
        }

        private void PickerSex_Unfocused(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Sex)) return;
        }

        private void EntryAge_Unfocused(object sender, EventArgs e)
        {
            if (!char.IsDigit(Convert.ToChar(Age)) && Age < 14 || Age > 99)
            {
                Toast.MakeText(App.Context, "Введен некорректный возраст", ToastLength.Long).Show();
                Age = 0;
            }
        }

        private async void Register(object sender, EventArgs e)
        {
            //Проверка введённых данных
            if (string.IsNullOrEmpty(Nickname) || char.IsWhiteSpace(Convert.ToChar(Age)) || Age == 0 || string.IsNullOrEmpty(Sex) ||
                string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PasswordRepeat))
            {
                Toast.MakeText(App.Context, "Заполнены не все поля", ToastLength.Long).Show();
                return;
            }

            //Генерация хэш-кода.
            string passwordHash = Crypto.CreateHashCode(PasswordRepeat);

            try
            {
                //При успешной или неудачной регистрации выведется соответствующее сообщение.
                string message = await Registration.RegistrationUser(Nickname, passwordHash, Age, Sex);
                Toast.MakeText(App.Context, message, ToastLength.Long).Show();
                await Navigation.PopAsync();
            }
            catch (Java.Net.SocketTimeoutException)
            {
                Toast.MakeText(App.Context, "Ошибка подключения к серверу", ToastLength.Long).Show();
            }
        }
    }
}