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
    public partial class OptionsPage : ContentPage
    {
        private string currentVersion;
        private string currentBuild;

        public string CurrentVersion
        {
            get { return currentVersion; }
            set
            {
                currentVersion = value;
                OnPropertyChanged("CurrentVersion");
            }
        }

        public string CurrentBuild
        {
            get { return currentBuild; }
            set
            {
                currentBuild = value;
                OnPropertyChanged("CurrentBuild");
            }
        }

        public OptionsPage()
        {
            InitializeComponent();
        }

        private async void ClearViewingBooks(object sender, EventArgs e)
        {
            User user = User.GetUser();

            if (user != null)
            {
                user.ViewingBooks = new List<Book>();

                object data = new
                {
                    viewing_books = user.ViewingBooks
                };

                await UsersOperations.UpdateUserData(user.Id, data);

                Toast.MakeText(App.Context, "История просмотров очищена", ToastLength.Long).Show();
            }
        }

        private async void OpenFeedbackPage(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(App.Context, "Отсутствует подключение к Интернету", ToastLength.Long).Show();
                return;
            }

            await Navigation.PushAsync(new FeedbackPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.CurrentVersion = VersionTracking.CurrentVersion;
            this.CurrentBuild = VersionTracking.CurrentBuild;
        }
    }
}