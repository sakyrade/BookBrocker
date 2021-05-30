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
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (User.GetUser() == null)
            {
                this.Content = new Views.SignInView()
                {
                    Margin = new Thickness(10)
                };
                Title = "Авторизация";
            }
            else
            {
                this.Content = new Views.AccountInfoView()
                {
                    Margin = new Thickness(10)
                };
                Title = "Аккаунт";
            }
        }
    }
}