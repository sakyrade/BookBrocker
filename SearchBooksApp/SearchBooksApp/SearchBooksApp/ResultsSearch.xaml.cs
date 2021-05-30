using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsSearch : ContentPage
    {
        private User user;
        private Book selectedBook;
        private List<Book> books;

        private string Query { get; set; }

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        public ResultsSearch(string query)
        {
            InitializeComponent();

            this.Query = query;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                this.Content = new Label()
                {
                    Text = "Отсутствует подключение к Интернету.",
                    TextColor = Color.Black,
                    FontFamily = "HDR",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                return;
            }

            try
            {
                if (books == null)
                    books = await SearchBooks.Search(Query);

                searchIndicator.IsRunning = false;
                searchIndicator.IsVisible = false;

                if (books.Count != 0)
                {
                    resultSearch.ItemsSource = books;
                    resultSearch.IsVisible = true;

                    user = User.GetUser();

                    if (user != null)
                    {
                        //User oldUser = User.GetUser(user);

                        if (user.SearchHistory.Where(sh => sh == Query).FirstOrDefault() == null)
                            user.SearchHistory.Add(Query);

                        object data = new
                        {
                            search_history = user.SearchHistory
                        };

                        await UsersOperations.UpdateUserData(user.Id, data);
                    }
                }
                else this.Content = new Label()
                {
                    Text = $"Результатов по запросу \"{Query}\" не найдено.",
                    TextColor = Color.Black,
                    FontFamily = "HDR",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
            }
            catch (Java.Net.SocketTimeoutException)
            {
                this.Content = new Label()
                {
                    Text = $"Ошибка подключения к серверу.",
                    TextColor = Color.Black,
                    FontFamily = "HDR",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
            }
        }

        private async void SelectionBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBook != null)
                await Navigation.PushAsync(new BookPage(resultSearch.SelectedItem as Book));

            resultSearch.SelectedItem = null;
        }
    }
}