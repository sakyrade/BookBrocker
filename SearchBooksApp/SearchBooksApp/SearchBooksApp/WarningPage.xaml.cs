using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBooksApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WarningPage : ContentPage
	{
		private Book book;

		public ICommand GoToBookPage => new Command(async () => {
			await Navigation.PushAsync(new BookPage(book));
			Navigation.RemovePage(this);
		});

		public WarningPage (Book book)
		{
			InitializeComponent();

			this.book = book;
		}
    }
}