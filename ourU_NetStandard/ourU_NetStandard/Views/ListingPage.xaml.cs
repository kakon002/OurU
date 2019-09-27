using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ourU_NetStandard.Views
{
    public partial class ListingPage : ContentPage
    {
        public Services.AzureMobileService azServ = new Services.AzureMobileService();
        public ObservableCollection<Models.Book> listings = new ObservableCollection<Models.Book>();

        public ListingPage()
        {
            InitializeComponent();
            var task1 = Task.Run(
            async () => await azServ.Initialize());
            task1.Wait();
            azServ.getListingsCollection(ref listings);
        }
    }
}
