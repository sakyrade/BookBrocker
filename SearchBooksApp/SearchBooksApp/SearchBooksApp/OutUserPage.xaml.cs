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
    public partial class OutUserPage : ContentPage
    {
        public OutUserPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(2000);

            User.Delete();
            App.Current.Properties.Remove("auth_token");
            await Shell.Current.GoToAsync($"//Account");
        }
    }
}