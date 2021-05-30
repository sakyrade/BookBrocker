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
    public partial class OptionsPage : ContentPage
    {
        public OptionsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (User.GetUser() != null)
                btnExit.IsVisible = true;
            else
                btnExit.IsVisible = false;
        }

        private async void ExitAccount(object sender, EventArgs e)
        {
            User.Delete();
            App.Current.Properties.Remove("auth_token");
            await Shell.Current.GoToAsync($"//Account");
        }
    }
}