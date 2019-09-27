using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ourU_NetStandard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookPage : ContentPage
    {
        Services.AzureMobileService azserv = new Services.AzureMobileService();
        public AddBookPage()
        {
            InitializeComponent();
            var task1 = Task.Run(
                async () => await azserv.Initialize());
            task1.Wait();
        }

        public async void ListBook_Clicked(object sender, System.EventArgs e)
        {

            string isbn = bookISBNEntry.Text;
            string title = bookTitleEntry.Text;
            string author = bookAuthorEntry.Text;
            string edition = bookEditionEntry.Text;

            Models.Book toAdd = new Models.Book
            {
                theID = isbn,
                theISBN = isbn,
                theTitle = title,
                theAuthor = author,
                theEdition = edition,
                isDeleted = false,
                isBook = true
            };

            bool success = await azserv.AddBook(toAdd);

            if (success)
                await DisplayAlert("Success", "Your book has been posted successfully!", "OK");
            else
                await DisplayAlert("Error", "There was a problem listing your book!", "OK");

        }

    }
}