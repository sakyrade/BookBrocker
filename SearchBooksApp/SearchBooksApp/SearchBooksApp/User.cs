using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace SearchBooksApp
{
    //Класс пользователей.
    public class User
    {
        [JsonIgnore]
        private string id;
        [JsonIgnore]
        private string nickname;
        [JsonIgnore]
        private string dateRegistration;
        [JsonIgnore]
        private string sex;
        [JsonIgnore]
        private string password;
        [JsonIgnore]
        private List<Book> selectedBooks;
        [JsonIgnore]
        private List<string> favoriteGenres;
        [JsonIgnore]
        private List<Book> recomendedBooks;
        [JsonIgnore]
        private List<string> searchHistory;
        [JsonIgnore]
        private List<Book> viewingBooks;
        [JsonIgnore]
        private string recomendedDate;
        //Применение паттерна Singleton.
        [JsonIgnore]
        private static User user;

        [JsonProperty(PropertyName = "_id")]
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged("Nickname");
            }
        }

        [JsonProperty(PropertyName = "date_registration")]
        public string DateRegistration
        {
            get { return dateRegistration; }
            set
            {
                dateRegistration = value;
                OnPropertyChanged("DateRegistration");
            }
        }

        [JsonProperty(PropertyName = "sex")]
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }
        }

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        [JsonProperty(PropertyName = "selected_books")]
        public List<Book> SelectedBooks
        {
            get { return selectedBooks; }
            set
            {
                selectedBooks = value;
                OnPropertyChanged("SelectedBooks");
            }
        }

        [JsonProperty(PropertyName = "favorite_genres")]
        public List<string> FavoriteGenres
        {
            get { return favoriteGenres; }
            set
            {
                favoriteGenres = value;
                OnPropertyChanged("FavoriteGenres");
            }
        }

        [JsonProperty(PropertyName = "recomended_books")]
        public List<Book> RecomendedBooks
        {
            get { return recomendedBooks; }
            set
            {
                recomendedBooks = value;
                OnPropertyChanged("RecomendedBooks");
            }
        }

        [JsonProperty(PropertyName = "get_recomended_date")]
        public string RecomendedDate
        {
            get { return recomendedDate; }
            set
            {
                recomendedDate = value;
                OnPropertyChanged("RecomendedDate");
            }
        }

        [JsonProperty(PropertyName = "search_history")]
        public List<string> SearchHistory
        {
            get { return searchHistory; }
            set
            {
                searchHistory = value;
                OnPropertyChanged("SearchHistory");
            }
        }

        [JsonProperty(PropertyName = "viewing_books")]
        public List<Book> ViewingBooks
        {
            get { return viewingBooks; }
            set
            {
                viewingBooks = value;
                OnPropertyChanged("ViewingBooks");
            }
        }

        //Метод для применения паттерна Singleton.
        public static User GetUser(User item = null)
        {
            if (user == null) user = item;
            return user;
        }

        //Удаление ссылки на текущий объект.
        public static void Delete()
        {
            user = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
