using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vemini.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace Vemini.AppNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErrandView : ContentPage
	{
		public ErrandView ()
		{
            InitializeComponent();
            getErrandsToList();
            DisplayCurLoc();
        }
        public async void DisplayCurLoc()
        {
            try
            {

                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Position p = new Position(location.Latitude, location.Longitude);
                    MapSpan mapspan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(.10));
                    map.MoveToRegion(mapspan);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void MenuItem_OnClickedPlus(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateErrand());
        }
        private  void GetGeoPermission()
        {
            var response =  CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar).Result;
        }

        private void getErrandsToList()
        {
            

            List<Errand> errandDbList = new List<Errand>();

            //-----getErrands in ErrandServices----//
            // Change Berlin to User town 
            errandDbList = ErrandService.GetErrands("berlin");
           //for testing
            //errandDbList.Add(TestCaseClass.GetExampleErrandShort());
            //errandDbList.Add(TestCaseClass.GetExampleErrandLong());
            //errandDbList.Add(TestCaseClass.GetExampleErrandShort());

            errand_listview.ItemsSource = errandDbList; //.Select(n => $"item-{n}");
            // errand_listview.ItemTemplate = new DataTemplate(ImageCell(ImageSource = this.)); //=>
            //{
            //    return new ImageCell ;
            //});
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var selectedErrand = e.Item as Errand;
            await Navigation.PushAsync(new DetailView(selectedErrand.Title,selectedErrand.Description, selectedErrand.AddressePlz, selectedErrand.AdresseStreet, selectedErrand.AdresseNmbr, selectedErrand.AdresseCity));
        }

        private void MenuItem_OnClickedMessages(object sender, EventArgs e)
        {
            //Dimitry Messages Page
        }
    }
}