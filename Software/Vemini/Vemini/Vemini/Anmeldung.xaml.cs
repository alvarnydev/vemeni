using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vemini
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Anmeldung : ContentPage
	{
		public Anmeldung ()
        {
            InitializeComponent();
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
            if (getEmail() == "" || getEmail() == null || getPassword() == "" || getPassword() == null)
            {
                DisplayAlert("Warnung","Emailadresse und/oder Passwort ungueltig", "ok");
            }
            else
            {
                //Uebermittlung an Datenbank
            }
        }

        private void Button_OnClickedPwVerg(object sender, EventArgs e)
        {
            //Rücksetzen per Email Versendung?
        }

        private async void Button_OnClickedRegist(object sender, EventArgs e)
        {
            //Öffnen des Registrierungsformular
            await Navigation.PushAsync(new Registrierung());
        }
    }
}