using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using ourU_NetStandard.Services;
using ourU_NetStandard.Views;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ourU_NetStandard
{
    public partial class App : Application
    {
        public static PublicClientApplication AuthenticationClient { get; private set; }
        public static IAuthenticate AuthenticationProvider { get; private set; }

        public static UIParent UiParent = null;

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }


        public static IAuthenticate Authenticator { get; private set; }
        public static string AzureBackendUrl = "https://ouru.azurewebsites.net";    
        public static MobileServiceClient client = new MobileServiceClient(AzureBackendUrl);
        
        public App()
        {
            //InitializeComponent();
            //AuthenticationClient = new PublicClientApplication(Constants.ApplicationID);
            MainPage = new NavigationPage(new LogInPage());
        }

        public static MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
