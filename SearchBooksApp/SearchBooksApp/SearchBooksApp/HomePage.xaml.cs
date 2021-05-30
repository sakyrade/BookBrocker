using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private Book selectedFictionBook;
        private Book selectedLessonBook;
        private List<Book> fictionBooksList;
        private List<Book> lessonsBooksList;

        public string TextQuery { get; set; }

        public Book SelectedFictionBook
        {
            get { return selectedFictionBook; }
            set
            {
                selectedFictionBook = value;
                OnPropertyChanged("SelectedFictionBook");
            }
        }

        public Book SelectedLessonBook
        {
            get { return selectedLessonBook; }
            set
            {
                selectedLessonBook = value;
                OnPropertyChanged("SelectedLessonBook");
            }
        }

        public ICommand SearchCommand => new Command<string>(async (string query) =>
        {
            await Navigation.PushAsync(new ResultsSearch(query));
        });

        public HomePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (fictionBooksList == null)
                fictionBooksList = await SearchBooks.GetBooksForHomePage("Художественная литература");

            if (lessonsBooksList == null)
                lessonsBooksList = await SearchBooks.GetBooksForHomePage("Образование");

            fictionBooks.ItemsSource = fictionBooksList;
            lessonsBooks.ItemsSource = lessonsBooksList;

            SelectedFictionBook = null;
            SelectedLessonBook = null;
        }

        private async void GoToRecomendedBooks(object sender, EventArgs e)
        {
            RecomendedBooksPage recomendedBooksPage = new RecomendedBooksPage();
            await Navigation.PushAsync(recomendedBooksPage);
        }

        private async void GoToFavoriteBooks(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//Favorite");
        }

        private async void SelectionFictionBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedFictionBook != null)
                await Navigation.PushAsync(new BookPage(SelectedFictionBook));
        }

        private async void SelectionLessonBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedLessonBook != null)
                await Navigation.PushAsync(new BookPage(SelectedLessonBook));
        }
    }
}