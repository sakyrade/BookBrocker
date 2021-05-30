using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritePage : ContentPage
    {
        private Book selectedBook;

        public User User { get; set; }

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        public FavoritePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            User = User.GetUser();

            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            nonAdded.IsVisible = false;
            nonAuth.IsVisible = false;
            favoriteBooks.IsVisible = false;

            if (User == null)
            {
                nonAuth.IsVisible = true;
                return;
            }

            if (User.SelectedBooks.Count != 0)
            {
                JObject response = await UsersOperations.UpdateUserData(User.Id);

                if (response != null)
                    User.SelectedBooks = JsonConvert.DeserializeObject<List<Book>>(response["selected_books"].ToString());

                favoriteBooks.ItemsSource = User.SelectedBooks;
                favoriteBooks.IsVisible = true;
            }
            else
                nonAdded.IsVisible = true;

            SelectedBook = null;
        }

        private async void SelectionBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBook != null)
                await Navigation.PushAsync(new BookPage(SelectedBook));
        }
    }
}