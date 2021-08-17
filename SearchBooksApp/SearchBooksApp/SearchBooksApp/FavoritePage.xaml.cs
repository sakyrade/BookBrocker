using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
                try
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        
                        JObject response = await UsersOperations.UpdateUserData(User.Id);

                        if (response != null)
                            User.SelectedBooks = JsonConvert.DeserializeObject<List<Book>>(response["selected_books"].ToString());
                    }
                    else
                        Toast.MakeText(App.Context, "Проверьте подключение к Интернету", ToastLength.Long).Show();

                    favoriteBooks.ItemsSource = User.SelectedBooks;
                    favoriteBooks.IsVisible = true;
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
                    return;
                }
            }
            else
                nonAdded.IsVisible = true;

            SelectedBook = null;
        }

        private async void SelectionBook(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBook != null)
            {
                if (SelectedBook.AgeLimit == "18")
                    await Navigation.PushAsync(new WarningPage(SelectedBook));
                else
                    await Navigation.PushAsync(new BookPage(SelectedBook));
            }
        }
    }
}