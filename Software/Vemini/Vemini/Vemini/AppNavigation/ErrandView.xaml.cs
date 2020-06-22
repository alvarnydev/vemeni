using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vemini.AppNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErrandView : ContentPage
	{
		public ErrandView ()
		{
			InitializeComponent ();

        }

        private async void MenuItem_OnClickedPlus(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateErrand());
        }
        private  void GetGeoPermission()
        {
            var response =  CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar).Result;
        }

    }
}