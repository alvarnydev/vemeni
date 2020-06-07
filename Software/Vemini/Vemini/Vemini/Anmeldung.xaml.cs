using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Vemin;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vemini
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Anmeldung : ContentPage
    {
        private Account account;
        private readonly AccountStore store;

        public Anmeldung()
        {
            InitializeComponent();
            store = AccountStore.Create();
        }

        public string getEmail()
        {
            return email_entry.Text;
        }

        public string getPassword()
        {
            return password_entry.Text;
        }

        private void Button_OnClickedAnmelden(object sender, EventArgs e)
        {
            if (getEmail() == "" || getEmail() == null || getPassword() == "" || getPassword() == null) DisplayAlert("Warnung", "Emailadresse und/oder Passwort ungueltig", "ok");
        }

        private void Button_OnClickedGoogleLogin(object sender, EventArgs e)
        {
            string clientId = null;
            string redirectUri = null;

            //Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null;            
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.GoogleiOSClientId;
                    redirectUri = Constants.GoogleiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.GoogleAndroidClientId;
                    redirectUri = Constants.GoogleAndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.GoogleScope,
                new Uri(Constants.GoogleAuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.GoogleAccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;



            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        private async void Button_OnClickedRegist(object sender, EventArgs e)
        {
            //Öffnen des Registrierungsformular
            await Navigation.PushAsync(new Registrierung());
        }

        [Obsolete]
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                    // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                    var request = new OAuth2Request("GET", new Uri(Constants.GoogleUserInfoUrl), null, e.Account);
                    var response = await request.GetResponseAsync();

                    if (response != null)
                    {
                        // Deserialize the data and store it in the account store
                        // The users email address will be used to identify data in SimpleDB

                        var userJson = await response.GetResponseTextAsync();
                        user = JsonConvert.DeserializeObject<User>(userJson);
                    }
                    if (account != null) store.Delete(account, Constants.AppName);

                    await store.SaveAsync(account = e.Account, Constants.AppName);

                    Application.Current.Properties.Remove("Id");
                    Application.Current.Properties.Remove("FirstName");
                    Application.Current.Properties.Remove("LastName");
                    Application.Current.Properties.Remove("DisplayName");
                    Application.Current.Properties.Remove("EmailAddress");
                    Application.Current.Properties.Remove("ProfilePicture");

                    Application.Current.Properties.Add("Id", user.Id);
                    Application.Current.Properties.Add("FirstName", user.GivenName);
                    Application.Current.Properties.Add("LastName", user.FamilyName);
                    Application.Current.Properties.Add("DisplayName", user.Name);
                    Application.Current.Properties.Add("EmailAddress", user.Email);
                    Application.Current.Properties.Add("ProfilePicture", user.Picture);

                    await Navigation.PushAsync(new ProfilePage());
               // }
            }
        }

        [Obsolete]
        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}