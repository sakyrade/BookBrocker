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
    public partial class AccountInfoView : ContentView
    {
        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public AccountInfoView()
        {
            InitializeComponent();
            User = User.GetUser();
        }

        private async void UpdatePassword(object sender, EventArgs e)
        {
            UpdatePasswordPage updatePasswordPage = new UpdatePasswordPage();
            await Navigation.PushAsync(updatePasswordPage);
        }

        private async void SelectGenres(object sender, EventArgs e)
        {
            FavoriteGenresPage favoriteGenresPage = new FavoriteGenresPage();
            await Navigation.PushAsync(favoriteGenresPage);
        }
    }
}