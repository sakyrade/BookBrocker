using Android.Content;
using SearchBooksApp.Droid;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    public partial class App : Application
    {
        public static Context Context { get; private set; }

        public App()
        {
            InitializeComponent();
            
            MainPage = new WallpaperPage();
            DependencyService.Get<IStatusBar>().HideStatusBar();
        }

        protected async override void OnStart()
        {
            Context = Android.App.Application.Context;

            try
            {
                if (!string.IsNullOrEmpty(App.Current.Properties["auth_token"].ToString()))
                    await Authorization.Auth(App.Current.Properties["auth_token"].ToString());
            }
            catch
            {

            }

            await Task.Delay(7000);
            MainPage = new MainPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
