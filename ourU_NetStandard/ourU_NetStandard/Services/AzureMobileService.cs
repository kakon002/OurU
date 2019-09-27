using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;

namespace ourU_NetStandard.Services
{
    public class AzureMobileService
    {
        IMobileServiceClient client;
        IMobileServiceSyncTable<Models.Book> bookTable;


        public async Task<bool> Initialize()
        {
            bool done = false;
            try
                {
                    client = new MobileServiceClient("https://ouru.azurewebsites.net");
                    const string path = "Book.db";
                    var store = new MobileServiceSQLiteStore(path);
                    store.DefineTable<Models.Book>();
                    await client.SyncContext.InitializeAsync(store);
                    bookTable = client.GetSyncTable<Models.Book>();
                    await bookTable.PurgeAsync();
                    await SyncAsync();
                    done = true;
                }

                catch (Exception e)
                {
                    string error = e.Message;
                }
            return done;
        }

        public async Task<bool> AddBook(Models.Book newBook)
        {
            try
            {

                await bookTable.InsertAsync(newBook);
                var result = await SyncAsync();
                if (result.Success == false)
                {
                    //do something
                }
                await bookTable.PurgeAsync();
                await SyncAsync();
                return true;
            }


            catch (Exception e)
            {
                string result = e.Message.ToString();
                return false;
            }

        }

        public interface IResult
        {
            bool Success { get; }
            string Message { get; }
        }

        public interface IResult<TValue>: IResult
        {
            TValue Value { get; }
        }

        public class Result : IResult
        {
            public string Message { get; }
            public bool Success { get; }
            public Result(
                bool success,
                string message
                )
            {
                Success = success;
                Message = message;
            }
        }

        public class Result<TValue> : Result, IResult<TValue>
        {
            public TValue Value { get; }
            public Result(
                bool success,
                string message,
                TValue value = default(TValue)
                ) : base(success, message)
            {
                Value = value;
            }
        }

        public async Task<IResult> SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();
                // The first parameter is a query name that is used internally by the client 
                // SDK to implement incremental sync.
                // Use a different query name for each unique query in your program.
                await bookTable.PullAsync("Books", bookTable.CreateQuery());
            }

            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            catch (Exception e)
            {
                return new Result(false, e.Message);
            }

            // Simple error/conflict handling.
            if (syncErrors != null)
            {
                var sb = new System.Text.StringBuilder();
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        // Update failed, revert to server's copy
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change
                        await error.CancelAndDiscardItemAsync();
                    }

                    var errorMessage = $"Error executing sync operation. Item: {error.TableName} ({error.Item["id"]}). Operation discarded.";
                    Debug.WriteLine(errorMessage);
                    sb.AppendLine(errorMessage);
                }
                return new Result(false, sb.ToString());
            }

            return new Result(true, "success");
        }

        public async Task clearDeleted()
        {
            await bookTable.PurgeAsync(bookTable.Where(book => book.isDeleted));
        }

        public async Task getBooksAsync(List<Models.Book> theList)
        {
            await SyncAsync();

            try
            {
                List<Models.Book> test = await bookTable.ToListAsync();
                foreach (var temp in test)
                {
                    if(temp.isBook)
                        theList.Add(temp);
                }
            }

            catch (Exception e)
            {
                string result = e.Message.ToString();
            }
        }

        public async Task<bool> getListings(ObservableCollection<Models.Book> listings)
        {

            try
            {

                await SyncAsync();
                List<Models.Book> theList = await bookTable.ToListAsync();
                listings = new ObservableCollection<Models.Book>(theList);
                return true;
            }

            catch (Exception e)
            {
                string result = e.Message.ToString();
            }
            return false;

        }

        public async Task<bool> deleteBook(string bookTitle)
        {
            try
            {
                await bookTable.PurgeAsync(bookTable.Where(book => 
                (book.theTitle == bookTitle) && (book.isBook == true)));
                return true;
            }
            catch (Exception e)
            {
                string err = e.Message;
            }

            return false;
        }

        public async Task<bool> deleteListing(string bookTitle, string email)
        {
            try
            {
                await bookTable.PurgeAsync(bookTable.Where(book =>
                    (book.theTitle == bookTitle) && (book.isListing == true)));
                return true;
            }
            catch (Exception e)
            {
                string err = e.Message;
            }
            return false;
        }

        public void getListingsCollection(ref ObservableCollection<Models.Book> listings)
        {
            List<Models.Book> firstList = new List<Models.Book>(); 
            var task1 = Task.Run(
                async () => await bookTable.ToListAsync());
            task1.Wait();
            firstList = task1.Result;
            foreach (Models.Book temp in firstList)
            {
                if (temp.isListing == true)
                    listings.Add(temp);
            }
        }

        public void getBookCollection(ref ObservableCollection<Models.Book> books)
        {
            List<Models.Book> list;
            //= new List<Models.Book>();
            var task1 = Task.Run(
            async () => await bookTable.ToListAsync());
            task1.Wait();
            list = task1.Result;
            foreach (Models.Book temp in list)
            {
                if (temp.isBook == true)
                    books.Add(temp);
            }
        }

        public void searchBooks(ref ObservableCollection<Models.Book> books, string bookTitle)
        {
            List<Models.Book> list = new List<Models.Book>();
            var task1 = Task.Run(
            async () => await bookTable.ToListAsync());
            task1.Wait();
            list = task1.Result;
            foreach (Models.Book temp in list)
            {
                if (temp.theTitle == bookTitle)
                    books.Add(temp);
            }
        }
    }
}
