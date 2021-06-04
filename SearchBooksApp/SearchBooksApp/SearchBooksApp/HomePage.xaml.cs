using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchBooksApp.Droid;
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
        private bool isRefreshing;
        private Book selectedFictionBook;
        private Book selectedLessonBook;
        private List<Book> fictionBooksList;
        private List<Book> lessonsBooksList;

        public string TextQuery { get; set; }

        public ICommand UpdateHomePage => new Command(() => {
            homeBooksListsIndicator.IsRunning = true;
            homeBooksListsIndicator.IsVisible = true;
            errorLabel.IsVisible = false;
            homeBooksLists.IsVisible = false;
            fictionBooksList = null;
            lessonsBooksList = null;
            OnAppearing();
            IsRefreshing = false;
        });

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

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
            DependencyService.Get<IStatusBar>().ShowStatusBar();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                homeBooksListsIndicator.IsRunning = false;
                homeBooksListsIndicator.IsVisible = false;
                errorLabel.Text = "Отсутствует подключение к Интернету.";
                errorLabel.IsVisible = true;
                return;
            }

            try
            {
                if (fictionBooksList == null)
                    fictionBooksList = await SearchBooks.GetBooksForHomePage("Художественная литература");

                if (lessonsBooksList == null)
                    lessonsBooksList = await SearchBooks.GetBooksForHomePage("Образование");


                fictionBooks.ItemsSource = fictionBooksList;
                lessonsBooks.ItemsSource = lessonsBooksList;
                homeBooksLists.IsVisible = true;
            }
            catch (Java.Net.SocketTimeoutException)
            {
                errorLabel.Text = "Ошибка подключения к серверу. Повторите попытку.";
                errorLabel.IsVisible = true;
            }
            finally
            {
                homeBooksListsIndicator.IsRunning = false;
                homeBooksListsIndicator.IsVisible = false;
            }

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
            {
                if (SelectedFictionBook.AgeLimit == "16")
                    await Navigation.PushAsync(new WarningPage(SelectedFictionBook));
                else
                    await Navigation.PushAsync(new BookPage(SelectedFictionBook));
            }
        }

        private async void SelectionLessonBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedLessonBook != null)
            {
                if (SelectedLessonBook.AgeLimit == "16")
                    await Navigation.PushAsync(new WarningPage(SelectedLessonBook));
                else
                    await Navigation.PushAsync(new BookPage(SelectedLessonBook));
            }
        }
    }
}