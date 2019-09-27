using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ourU_NetStandard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        public Services.AzureMobileService azServ = new Services.AzureMobileService();
        public ObservableCollection<Models.Book> bookList = new ObservableCollection<Models.Book>();

        public SearchPage ()
		{
			InitializeComponent ();
            BookListView.ItemsSource = bookList;
            azServ = new Services.AzureMobileService();
            var task1 = Task.Run(
            async () => await azServ.Initialize());
            task1.Wait();
            azServ.getBookCollection(ref bookList);
		}
        async void BookItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BookItemPage());
        }

        async void AddBook_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddBookPage());
        }

	}
}