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
    public partial class RecomendedBooksPage : ContentPage
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

        public RecomendedBooksPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            User = User.GetUser();

            if (User == null)
            {
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
                nonAuth.IsVisible = true;
                return;
            }

            await Task.Delay(5000);

            if (Convert.ToDateTime(User.RecomendedDate).Date < DateTime.Now.Date || Convert.ToDateTime(User.RecomendedDate).Date > DateTime.Now.Date)
            {
                var response = await RecomendedBooks.GetRecomendedBooks(User.Id);
                User.RecomendedBooks = JsonConvert.DeserializeObject<List<Book>>(response["books"].ToString());
                User.RecomendedDate = response["recomended_date"].ToString();
            }

            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            recomendedBooks.IsVisible = true;
            recomendedBooks.ItemsSource = User.RecomendedBooks;
            SelectedBook = null;
        }

        private async void SelectionBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBook != null)
                await Navigation.PushAsync(new BookPage(SelectedBook));
        }
    }
}