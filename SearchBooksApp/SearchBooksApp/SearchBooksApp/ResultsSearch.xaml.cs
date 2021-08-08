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
        private bool isSearchByISBN;

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

        public ResultsSearch(string query, bool isSearchByISBN = false)
        {
            InitializeComponent();

            this.Query = query;
            this.isSearchByISBN = isSearchByISBN;
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
                {
                    if (!isSearchByISBN)
                        books = await SearchBooks.Search(Query);
                    else
                        books = await SearchBooks.SearchByISBN(Query);
                }

                searchIndicator.IsRunning = false;
                searchIndicator.IsVisible = false;
                searchAlert.IsVisible = false;

                if (books.Count != 0)
                {
                    resultSearch.ItemsSource = books;
                    resultSearch.IsVisible = true;

                    user = User.GetUser();

                    if (user != null)
                    {
                        //User oldUser = User.GetUser(user);
                        if (!isSearchByISBN)
                        {
                            if (user.SearchHistory.Where(sh => sh == Query).FirstOrDefault() == null)
                                user.SearchHistory.Add(Query);

                            object data = new
                            {
                                search_history = user.SearchHistory
                            };

                            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                                await UsersOperations.UpdateUserData(user.Id, data);
                        }
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
            catch (System.Net.WebException)
            {
                this.Content = new Label()
                {
                    Text = "Сервер недоступен.",
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
                    Text = "Ошибка подключения к серверу.",
                    TextColor = Color.Black,
                    FontFamily = "HDR",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
            }
            catch (Java.Net.SocketException)
            {
                this.Content = new Label()
                {
                    Text = "Ошибка подключения к серверу.",
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
            {
                var book = resultSearch.SelectedItem as Book;

                if (book.AgeLimit == "18")
                    await Navigation.PushAsync(new WarningPage(book));
                else
                    await Navigation.PushAsync(new BookPage(book));
            }

            resultSearch.SelectedItem = null;
        }
    }
}