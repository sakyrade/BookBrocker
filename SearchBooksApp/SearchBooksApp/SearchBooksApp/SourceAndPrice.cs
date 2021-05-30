using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchBooksApp
{
    public class SourceAndPrice : BaseNotifyPropertyChanged
    {
        [JsonIgnore]
        private string bookSrc;
        [JsonIgnore]
        private double price;
        [JsonIgnore]
        private string site;

        [JsonProperty(PropertyName = "bookSrc")]
        public string BookSource
        {
            get { return bookSrc; }
            set
            {
                bookSrc = value;
                OnPropertyChanged("BookSource");
            }
        }

        [JsonProperty(PropertyName = "price")]
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        [JsonProperty(PropertyName = "site")]
        public string Site
        {
            get { return site; }
            set
            {
                site = value;
                OnPropertyChanged("Site");
            }
        }
    }
}
