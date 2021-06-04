using SearchBooksApp.Droid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SearchBooksApp
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();            
        }

        /*
        private async void TabBar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (User.GetUser() == null)
            {
                await Shell.Current.GoToAsync($"//Account");
                Android.Widget.Toast.MakeText(Android.App.Application.Context, "Чтобы просмотреть список избранного - войдите в аккаунт.", Android.Widget.ToastLength.Long).Show();
            }
        }
        */
    }
}
