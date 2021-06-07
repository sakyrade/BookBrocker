﻿using Android.Widget;
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
    public partial class BookPage : ContentPage
    {
        private Book book;
        private Book bookExists;
        private User user = User.GetUser();
        private SourceAndPrice sourceAndPrice;
        private string selectedImage;

        public string SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }

        public SourceAndPrice SourceAndPrice
        {
            get { return sourceAndPrice; }
            set
            {
                sourceAndPrice = value;
                OnPropertyChanged("SourceAndPrice");
            }
        }

        public Book Book
        {
            get { return book; }
            set
            {
                book = value;
                OnPropertyChanged("Book");
            }
        }

        public BookPage(Book book)
        {
            InitializeComponent();

            Book = book;

            bookImages.ItemsSource = book.ImageSources;
            SelectedImage = book.ImageSources.FirstOrDefault();

            var minSourceAndPriceBook = Book.SourceAndPriceBooks.FirstOrDefault();

            foreach (var sp in Book.SourceAndPriceBooks.ToList())
            {
                if (minSourceAndPriceBook.Price > sp.Price)
                {
                    Book.SourceAndPriceBooks.Remove(minSourceAndPriceBook);
                    Book.SourceAndPriceBooks.Add(minSourceAndPriceBook);
                    minSourceAndPriceBook = sp;
                }
                    
            }

            var minBookPrice = Book.SourceAndPriceBooks.FirstOrDefault();
            SourceAndPrice = minBookPrice;

            var otherBooks = Book.SourceAndPriceBooks.Where(b => b != sourceAndPrice).ToList();

            if (otherBooks.Count != 0)
            {
                sourcesAndPricesBooksFrame.IsVisible = true;
                sourcesAndPricesBooks.ItemsSource = otherBooks;
            }

            if (user != null)
            {
                bookExists = user.SelectedBooks.Where(b => b == Book).FirstOrDefault();

                if (bookExists != null)
                    addOrDeleteBook.Source = "frame_favorite2_x2";
            }
        }

        private async void AddBook(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Android.Widget.Toast.MakeText(App.Context, "Отсутствует подключение к Интернету", Android.Widget.ToastLength.Long).Show();
                return;
            }

            if (user == null)
            {
                Android.Widget.Toast.MakeText(App.Context, "Войдите в аккаунт или зарегистрируйтесь", Android.Widget.ToastLength.Long).Show();
                await Shell.Current.GoToAsync($"//Account");
                return;
            }

            bookExists = user.SelectedBooks.Where(b => b == Book).FirstOrDefault();

            //User oldUser = User.GetUser(user);
            try
            {
                if (bookExists == null)
                {
                    user.SelectedBooks.Add(Book);
                    addOrDeleteBook.Source = "frame_favorite2_x2";
                }
                else
                {
                    user.SelectedBooks.Remove(Book);
                    addOrDeleteBook.Source = "frame_favorite_x2";
                }

                object data = new
                {
                    selected_books = user.SelectedBooks
                };

                await UsersOperations.UpdateUserData(user.Id, data);
            }
            catch (System.Net.WebException)
            {
                Toast.MakeText(App.Context, "Сервер недоступен", ToastLength.Long).Show();
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await Navigation.PopAsync();
        }

        private async void SourcesAndPricesBooksSelect(object sender, SelectionChangedEventArgs e)
        {
            if (sourcesAndPricesBooks.SelectedItem == null) return;

            await Launcher.OpenAsync(((SourceAndPrice)sourcesAndPricesBooks.SelectedItem).BookSource);

            sourcesAndPricesBooks.SelectedItem = null;
        }

        private async void OpenFromBrowser(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(SourceAndPrice.BookSource);
        }
    }
}