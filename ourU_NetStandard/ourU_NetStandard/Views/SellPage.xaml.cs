using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ourU_NetStandard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellPage : ContentPage
    {
        public SellPage()
        {
            InitializeComponent();
        }

        async void Listing_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ListingPage());
        }
    }
}