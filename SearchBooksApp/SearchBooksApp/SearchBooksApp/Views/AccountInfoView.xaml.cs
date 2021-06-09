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
        private Book selectedBook;
        private List<Book> v_Books;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

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

            v_Books = User.ViewingBooks;

            try
            {
                if (v_Books.Count != 0)
                {
                    viewingBooks.ItemsSource = v_Books;
                    viewingBooks.IsVisible = true;
                }
                else
                {
                    textError.Text = "Пусто.";
                    textError.IsVisible = true;
                }
            }
            catch (System.Net.WebException)
            {
                textError.Text = "Сервер недоступен.";
                textError.IsVisible = true;
            }
            catch (Java.Net.SocketTimeoutException)
            {
                textError.Text = "Ошибка подключения к серверу.";
                textError.IsVisible = true;
            }
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

        private async void ExitAccount(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new OutUserPage());
        }

        private async void SelectionBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBook != null)
            {
                var book = viewingBooks.SelectedItem as Book;

                if (book.AgeLimit == "18")
                    await Navigation.PushAsync(new WarningPage(book));
                else
                    await Navigation.PushAsync(new BookPage(book));
            }

            viewingBooks.SelectedItem = null;
        }
    }
}