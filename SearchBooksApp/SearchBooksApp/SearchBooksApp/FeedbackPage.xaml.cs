using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        private string nicknameOrEmailAddress;
        private string theme;
        private string message;

        public string NicknameOrEmailAddress
        {
            get { return nicknameOrEmailAddress; }
            set
            {
                nicknameOrEmailAddress = value;
                OnPropertyChanged("NicknameOrEmailAddress");
            }
        }

        public string Theme
        {
            get { return theme; }
            set
            {
                theme = value;
                OnPropertyChanged("Theme");
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public FeedbackPage()
        {
            InitializeComponent();
        }

        private void EntryMessageTextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryMessage.Text != null)
            {
                if (entryMessage.Text.Length < 60) labelCountSymbols.TextColor = Color.FromHex("#E81B1B");
                if (entryMessage.Text.Length >= 60) labelCountSymbols.TextColor = Color.FromHex("16F0A0");
                if (entryMessage.Text.Length == 0) labelCountSymbols.TextColor = Color.Default;
            }
        }

        private void SendFeedback(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(App.Context, "Отсутствует подключение к Интернету", ToastLength.Long).Show();
                return;
            }

            if (string.IsNullOrEmpty(NicknameOrEmailAddress) || string.IsNullOrEmpty(Theme) || string.IsNullOrEmpty(Message))
            {
                Toast.MakeText(App.Context, "Заполнены не все поля", ToastLength.Long).Show();
                return;
            }

            if (Message.Length < 60)
            {
                Toast.MakeText(App.Context, "Слишком короткое сообщение", ToastLength.Long).Show();
                return;
            }

            try
            {
                Feedback feedback = new Feedback()
                {
                    NicknameOrEmailAddress = this.NicknameOrEmailAddress,
                    Theme = this.Theme,
                    Message = this.Message
                };

                feedback.SendFeedback();

                Toast.MakeText(App.Context, "Спасибо за отзыв!", ToastLength.Long).Show();
            }
            catch (System.Net.WebException)
            {
                Toast.MakeText(App.Context, "Сервер недоступен", ToastLength.Long).Show();
            }
            catch (Java.Net.SocketTimeoutException)
            {
                Toast.MakeText(App.Context, "Ошибка подключения к серверу", ToastLength.Long).Show();
            }
        }
    }
}