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
        }

        private void OnScanBookResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new ResultsSearch(result.Text, true)));
        }
    }
}