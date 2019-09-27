using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ourU_NetStandard.Views
{
    public partial class AddListing : ContentPage
    {
        public Services.AzureMobileService azserv = new Services.AzureMobileService();
        public AddListing()
        {
            InitializeComponent();
            var task1 = Task.Run(
                async () => await azserv.Initialize());
            task1.Wait();
        }

        public async void ListIt_Clicked(object sender, System.EventArgs e)
        {

            string isbn = bookIsbnEntry.Text;
            string price = bookPriceEntry.Text;
            string status = bookStatusEntry.Text;
            string comment = bookCommentEntry.Text;
            string name = userNameEntry.Text;
            string phone = userPhone.Text;
            string email = userEmail.Text;

            Models.Book toAdd = new Models.Book
            {
                theISBN = isbn,
                thePrice = price,
                theStatus = status,
                userComment = comment,
                userName = name,
                phoneNumber = phone,
                emailAddress = email,
                isDeleted = false,
                isBook = false,
                isListing = true
            };

            bool success = await azserv.AddBook(toAdd);

            if (success)
                await DisplayAlert("Success", "Your book has been posted successfully!", "OK");
            else
                await DisplayAlert("Error", "There was a problem listing your book!", "OK");

        }
    }
}
