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
    public partial class FavoriteGenresPage : ContentPage
    {
        private Label selectedLabel;

        private Label[] genres = new Label[]
        {
            new Label()
                {
                    Text = "Художественная литература",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Книги для детей",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Образование",
                    FontFamily = "HDR"
            },
            new Label()
                {
                    Text = "Наука и техника",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Общество",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Деловая литература",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Красота. Здоровье. Спорт",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Увлечения",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Психология",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Эзотерика",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Философия и религия",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Искусство",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Подарочные издания",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Книги на иностранных языках",
                    FontFamily = "HDR"
                },
            new Label()
                {
                    Text = "Аудиокниги",
                    FontFamily = "HDR"
                }
        };

        private User user;

        public Label SelectedLabel
        {
            get { return selectedLabel; }
            set
            {
                selectedLabel = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        public FavoriteGenresPage()
        {
            InitializeComponent();

            resultSearch.ItemsSource = genres;
            user = User.GetUser();

            foreach (var fg in user.FavoriteGenres)
            {
                foreach (var g in resultSearch.ItemsSource)
                {
                    Label genre = (Label)g;
                    if (fg.Equals(genre.Text)) genre.TextColor = Color.FromHex("#E56C44");
                }
            }
        }

        private async void SelectionGenre(object sender, SelectionChangedEventArgs e)
        {
            //var oldUser = User.GetUser(user);

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Android.Widget.Toast.MakeText(App.Context, "Отсутствует подключение к Интернету", Android.Widget.ToastLength.Long).Show();
                return;
            }

            if (SelectedLabel == null) return;

            if (user.FavoriteGenres.Contains(SelectedLabel.Text))
            {
                user.FavoriteGenres.Remove(SelectedLabel.Text);
                SelectedLabel.TextColor = Color.Default;
            }
            else
            {
                user.FavoriteGenres.Add(SelectedLabel.Text);
                SelectedLabel.TextColor = Color.FromHex("#E56C44");
            }

            object data = new
            {
                favorite_genres = user.FavoriteGenres
            };

            await UsersOperations.UpdateUserData(user.Id, data);

            resultSearch.SelectedItem = null;
        }
    }
}