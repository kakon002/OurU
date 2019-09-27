using System;

using Xamarin.Forms;

namespace ourU_NetStandard.Views
{
    public class ContentPage : ContentPage
    {
        public ContentPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

