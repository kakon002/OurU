using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace ourU_NetStandard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogInPage : ContentPage
	{
         public interface IAuthenticate
        {
            Task<bool> Authenticate();
        }

        public static IAuthenticate Authenticator { get; private set; }
        public static IAuthenticate AuthenticationProvider { get; private set; }
        public static string AzureBackendUrl = "https://ouru.azurewebsites.net";    
        public static MobileServiceClient MobileService = new MobileServiceClient(AzureBackendUrl);


        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        // Track whether the user has authenticated.
        bool authenticated = false;
        private MobileServiceUser user;

        public LogInPage()
        {
            InitializeComponent();
        }

       async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
            {
                authenticated = await App.Authenticator.LoginAsync();
            }

            if (authenticated == true)
            {
                Navigation.InsertPageBefore(new OurUPage(), this);
                await Navigation.PopAsync();
            }
        }

        async void TabbedPage(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TabContainer());
        }
    }
}