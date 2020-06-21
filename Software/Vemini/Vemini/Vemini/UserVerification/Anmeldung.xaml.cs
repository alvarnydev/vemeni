using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Vemin;
using Vemini.AppNavigation;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * Author: Benedikt Blank with the help of https://github.com/CuriousDrive/PublicProjects/tree/master/OAuthNativeFlow
 * Implemented: 04.06.20
 * Login page for User where he can login/register with his/her facebook/google account
 *
 */
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

        //retrieve Email from Textbox
        public string getEmail()
        {
            return email_entry.Text;
        }

        //retrieve Passwort from Textbox
        public string getPassword()
        {
            return password_entry.Text;
        }

        //Gets redirected to the facebook api where he can login to his/her account 
        private void Button_OnClickedFacebook(object sender, EventArgs e)
        {
            string clientId = null;
            string redirectUri = null;

            //clientId and redirectId depending on OS
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.FacebookiOSClientId;
                    redirectUri = Constants.FacebookiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.FacebookAndroidClientId;
                    redirectUri = Constants.FacebookAndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            
            //Authenticator for getting the access token for oauth
            var authenticator = new OAuth2Authenticator(
                clientId,
                Constants.FacebookScope,
                new Uri(Constants.FacebookAuthorizeUrl),
                new Uri(Constants.FacebookAccessTokenUrl),
                null);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            //Opens up facebook login page
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        private async void Button_OnClickedGoogleLogin(object sender, EventArgs e)
        {
            string clientId = null;
            string redirectUri = null;

            //clientId and redirectId depending on OS        
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

            //Authenticator for getting the access token for oauth
            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.GoogleScope,
                new Uri(Constants.GoogleAuthorizeUrl),
                new Uri(redirectUri),  //Location where the User gets redeirected after authenticating
                new Uri(Constants.GoogleAccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            //Opens up facebook login page
            var presenter = new OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        private async void Button_OnClickedRegist(object sender, EventArgs e)
        {
            //Öffnen des Registrierungsformular
            await Navigation.PushAsync(new Registrierung());
        }

        //If oauth access was granted Properties from the facebook/google -account get retrieved
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
            if (e.IsAuthenticated){

                //Different for facebook, otherwise for google
                if (authenticator.AuthorizeUrl.Host == "www.facebook.com")
                {
                    FacebookEmail facebookEmail = null;

                    var httpClient = new HttpClient();

                    var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=id,name,first_name,last_name,email,picture.type(large)&access_token=" + e.Account.Properties["access_token"]);

                    facebookEmail = JsonConvert.DeserializeObject<FacebookEmail>(json);

                    await store.SaveAsync(account = e.Account, Constants.AppName);

                    Application.Current.Properties.Remove("Id");
                    Application.Current.Properties.Remove("FirstName");
                    Application.Current.Properties.Remove("LastName");
                    Application.Current.Properties.Remove("DisplayName");
                    Application.Current.Properties.Remove("EmailAddress");
                    Application.Current.Properties.Remove("ProfilePicture");

                    Application.Current.Properties.Add("Id", facebookEmail.Id);
                    Application.Current.Properties.Add("FirstName", facebookEmail.First_Name);
                    Application.Current.Properties.Add("LastName", facebookEmail.Last_Name);
                    Application.Current.Properties.Add("DisplayName", facebookEmail.Name);
                    Application.Current.Properties.Add("EmailAddress", facebookEmail.Email);
                    Application.Current.Properties.Add("ProfilePicture", facebookEmail.Picture.Data.Url);

                    await Navigation.PushAsync(new ErrandView());
                }
                //google
                else 
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

                   if (account != null)
                   {
                       store.Delete(account, Constants.AppName);
                   }

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
                    Application.Current.Properties.Add("DisplayName", user.UserName);
                    Application.Current.Properties.Add("EmailAddress", user.Email);
                    Application.Current.Properties.Add("ProfilePicture", user.Picture);


                    await Navigation.PushAsync(new ErrandView());
                   
                }
            }
        }

        //If error with oauth occurs
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