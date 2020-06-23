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

namespace Vemini.AppNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErrandView : ContentPage
	{
		public ErrandView ()
		{
            InitializeComponent();
            getErrandsToList();

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
            errandDbList.Add(TestCaseClass.GetExampleErrand());
            errandDbList.Add(TestCaseClass.GetExampleErrand());
            errandDbList.Add(TestCaseClass.GetExampleErrand());
            //-------------Substitute--------------//

            errand_listview.ItemsSource = errandDbList.Select(n => $"item-{n}");
           // errand_listview.ItemTemplate = new DataTemplate(ImageCell(ImageSource = this.)); //=>
            //{
            //    return new ImageCell ;
            //});
        }
    }
}