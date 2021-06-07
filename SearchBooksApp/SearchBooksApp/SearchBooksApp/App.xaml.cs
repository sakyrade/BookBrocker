using Android.Content;
using Android.Widget;
using SearchBooksApp.Droid;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    object token;

                    if (App.Current.Properties.TryGetValue("auth_token", out token))
                        await Authorization.Auth(token.ToString());
                }
            }
            catch (System.Net.WebException)
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
