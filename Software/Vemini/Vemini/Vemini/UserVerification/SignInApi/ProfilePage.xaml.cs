using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


/*
 * Author: Benedikt Blank with the help of https://github.com/CuriousDrive/PublicProjects/tree/master/OAuthNativeFlow
 * Implemented: 04.06.20
 * Example display page of gathered User data from api
 *
 */
namespace Vemini
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        public ProfilePage()

        {

            InitializeComponent();



            if (Application.Current.Properties["ProfilePicture"] != null)

                imgProfilePicture.Source = Application.Current.Properties["ProfilePicture"].ToString();



            if (Application.Current.Properties["Id"] != null)

                lblIdValue.Text = lblIdValue.Text + Application.Current.Properties["Id"].ToString();



            if (Application.Current.Properties["FirstName"] != null)

                lblFirstNameValue.Text = lblFirstNameValue.Text + Application.Current.Properties["FirstName"].ToString();



            if (Application.Current.Properties["LastName"] != null)

                lblLastNameValue.Text = lblLastNameValue.Text + Application.Current.Properties["LastName"].ToString();



            if (Application.Current.Properties["EmailAddress"] != null)

                lblEmailAddressValue.Text = lblEmailAddressValue.Text + Application.Current.Properties["EmailAddress"].ToString();

        }
    }
}