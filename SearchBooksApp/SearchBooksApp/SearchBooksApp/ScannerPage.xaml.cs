using Android;
using Android.Content.PM;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ZXing.Net.Mobile.Forms.ZXingScannerPage
    {
        public ScannerPage()
        {
            InitializeComponent();

            if (App.Context.CheckSelfPermission(Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                this.Content = new Label()
                {
                    Text = "Разрешите использование камеры вашего устройства, перезапустите приложение и повторите попытку.",
                    TextColor = Color.Black,
                    FontFamily = "HDR",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
            }
        }

        private void OnScanBookResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new ResultsSearch(result.Text, true)));
        }
    }
}