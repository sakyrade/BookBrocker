using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePasswordPage : ContentPage
    {
        private string oldPassword;
        private string newPassword;
        private string passwordRepeat;
        private User user;

        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }

        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                OnPropertyChanged("NewPassword");
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

        public UpdatePasswordPage()
        {
            InitializeComponent();

            user = User.GetUser();
        }

        private void EntryOldPassword_Unfocused(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OldPassword)) return;

            if (Crypto.CreateHashCode(OldPassword) != user.Password)
            {
                Toast.MakeText(App.Context, "Это не старый пароль", ToastLength.Long).Show();
                OldPassword = null;
                return;
            }
        }

        private void EntryNewPassword_Unfocused(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewPassword)) return;

            if (NewPassword.Length < 6)
            {
                Toast.MakeText(App.Context, "Пароль должен быть больше 6 символов", ToastLength.Long).Show();
                NewPassword = null;
                return;
            }

            if (Crypto.CreateHashCode(NewPassword) == user.Password)
            {
                Toast.MakeText(App.Context, "Введенный пароль совпадает со старым", ToastLength.Long).Show();
                NewPassword = null;
                return;
            }
        }

        private void EntryPasswordRepeat_Unfocused(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordRepeat)) return;

            if (string.IsNullOrEmpty(NewPassword))
            {
                Toast.MakeText(App.Context, "Вы не ввели пароль", ToastLength.Long).Show();
                PasswordRepeat = null;
                return;
            }

            if (NewPassword != PasswordRepeat)
            {
                Toast.MakeText(App.Context, "Пароли не совпадают", ToastLength.Long).Show();
                PasswordRepeat = null;
            }
        }

        private async void UpdatePassword(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(App.Context, "Отсутствует подключение к Интернету", ToastLength.Long).Show();
                return;
            }

            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(PasswordRepeat))
            {
                Toast.MakeText(App.Context, "Заполнены не все поля", ToastLength.Long).Show();
                return;
            }

            //User oldUser = User.GetUser(user);
            //user.Password = Crypto.CreateHashCode(NewPassword);
            object data = new
            {
                password = Crypto.CreateHashCode(NewPassword)
            };

            try
            {
                await UsersOperations.UpdateUserData(user.Id, data);
            }
            catch (Java.Net.SocketTimeoutException)
            {
                Toast.MakeText(App.Context, "Ошибка подключения к серверу.", ToastLength.Long).Show();
                return;
            }
            Toast.MakeText(App.Context, "Пароль изменен", ToastLength.Long).Show();
            await Navigation.PopAsync();
        }
    }
}