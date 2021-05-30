using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchBooksApp
{
    public class Book : BaseNotifyPropertyChanged
    {
        [JsonIgnore]
        private string id;
        [JsonIgnore]
        private string imgSrc;
        [JsonIgnore]
        private string title;
        [JsonIgnore]
        private string author;
        [JsonIgnore]
        private List<SourceAndPrice> srcAndPriceBooks;
        [JsonIgnore]
        private string pubHouse;
        [JsonIgnore]
        private string genre;
        [JsonIgnore]
        private string yearRelease;
        [JsonIgnore]
        private string ageLimit;
        [JsonIgnore]
        private string isbn;
        [JsonIgnore]
        private string classBook;
        [JsonIgnore]
        private string typeBook;
        [JsonIgnore]
        private List<string> searchQueries;
        [JsonIgnore]
        private int countSearch;
        [JsonIgnore]
        private List<string> imageSources; 

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

        [JsonProperty(PropertyName = "img")]
        public string ImageSource
        {
            get { return imgSrc; }
            set
            {
                imgSrc = value;
                OnPropertyChanged("ImageSource");
            }
        }

        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        [JsonProperty(PropertyName = "author")]
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        [JsonProperty(PropertyName = "srcAndPriceBooks")]
        public List<SourceAndPrice> SourceAndPriceBooks
        {
            get { return srcAndPriceBooks; }
            set
            {
                srcAndPriceBooks = value;
                OnPropertyChanged("SourceAndPriceBook");
            }
        }

        [JsonProperty(PropertyName = "publishingHouse")]
        public string PublishingHouse
        {
            get { return pubHouse; }
            set
            {
                pubHouse = value;
                OnPropertyChanged("PublishingHouse");
            }
        }

        [JsonProperty(PropertyName = "genre")]
        public string Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                OnPropertyChanged("Genre");
            }
        }

        [JsonProperty(PropertyName = "yearRelease")]
        public string YearRelease
        {
            get { return yearRelease; }
            set
            {
                yearRelease = value;
                OnPropertyChanged("YearRelease");
            }
        }

        [JsonProperty(PropertyName = "ISBN")]
        public string ISBN
        {
            get { return isbn; }
            set
            {
                isbn = value;
                OnPropertyChanged("ISBN");
            }
        }

        [JsonProperty(PropertyName = "ageLimit")]
        public string AgeLimit
        {
            get { return ageLimit; }
            set
            {
                ageLimit = value;
                OnPropertyChanged("AgeLimit");
            }
        }

        [JsonProperty(PropertyName = "classBook")]
        public string ClassBook
        {
            get { return classBook; }
            set
            {
                classBook = value;
                OnPropertyChanged("ClassBook");
            }
        }

        [JsonProperty(PropertyName = "typeBook")]
        public string TypeBook
        {
            get { return typeBook; }
            set
            {
                typeBook = value;
                OnPropertyChanged("TypeBook");
            }
        }

        [JsonProperty(PropertyName = "searchQueries")]
        public List<string> SearchQueries
        {
            get { return searchQueries; }
            set
            {
                searchQueries = value;
                OnPropertyChanged("SearchQueries");
            }
        }

        [JsonProperty(PropertyName = "countSearch")]
        public int CountSearch
        {
            get { return countSearch; }
            set
            {
                countSearch = value;
                OnPropertyChanged("CountSearch");
            }
        }

        [JsonProperty(PropertyName = "imageSources")]
        public List<string> ImageSources
        {
            get { return imageSources; }
            set
            {
                imageSources = value;
                OnPropertyChanged("ImageSources");
            }
        }
    }
}
