using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Net.Wifi.Hotspot2;
using Android.Provider;
using Newtonsoft.Json;
using Vemini.AppNavigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vemini
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registrierung : ContentPage
	{

        private static readonly HttpClient client = new HttpClient();

        public Registrierung ()
		{
			InitializeComponent ();
		}

        private async void Button_OnClickedRegist(object sender, EventArgs e)
        {

            // Validate input
            if (await validateStrings())
            {
                // Create user object
                var user = new User
                {

                    UserName = UserEntry.Text,
                    Password = PwEntry.Text,
                    FirstName = FirstNameEntry.Text,
                    LastName = LastNameEntry.Text,
                    Phone = PhoneNumberEntry.Text,
                    Email = EmailEntry.Text,
                    PLZ = PlzEntry.Text,
                    City = CityEntry.Text,
                    Street = StreetEntry.Text,
                    StreetNumber = StreetnumberEntry.Text

                };

                // Serialize object
                string jsonContent = JsonConvert.SerializeObject(user);

                // Formulate request
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Constants.RegisterUrl, stringContent);

                // Update user info
                var responseString = await response.Content.ReadAsStringAsync();

                // Check response
                if (response.IsSuccessStatusCode)
                {

                    // Display success
                    await DisplayAlert("Registrierung erfolgreich", "Dein Account wurde erfolgreich angelegt.", "OK");

                    // Navigate forward
                    await Navigation.PushAsync(new Anmeldung());

                }
                else
                {

                    // Display error
                    await DisplayAlert("Registrierung fehlgeschlagen", $"Ein Fehler auf unserer Seite ist aufgetreten: {responseString}", "OK");

                }
            }
        }

        private async Task<bool> validateStrings()
        {
            // Empty string validation
            if (String.IsNullOrWhiteSpace(FirstNameEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Vornamen eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(LastNameEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Nachnamen eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(StreetEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Straße eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(PlzEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Postleitzahl eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(CityEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Stadt eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(PhoneNumberEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Telefonnummer eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(UserEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Nutzernamen eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(EmailEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte E-Mail-Adresse eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(PwEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Passwort eingeben.", "OK");
            else if (String.IsNullOrWhiteSpace(PwConfirmEntry.Text)) await DisplayAlert("Fehlende Eingabe", "Bitte Passwort bestätigen.", "OK");

            // Password validation
            else if (PwEntry.Text != PwConfirmEntry.Text)
                await DisplayAlert("Fehler bei Eingabe", "Die eingegebenen Passwörter stimmen nicht überein.", "OK");

            // If all checks were passed
            else return true;


            return false;

        }

    }
}