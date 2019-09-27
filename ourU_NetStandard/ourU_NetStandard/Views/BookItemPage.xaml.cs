using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ourU_NetStandard.Views
{
    public partial class BookItemPage : ContentPage
    {

        public BookItemPage()
        {
            InitializeComponent();
        }

        async void AddListing_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddListing());
        }

        async void Listing_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ListingPage());
        }
    }
}
