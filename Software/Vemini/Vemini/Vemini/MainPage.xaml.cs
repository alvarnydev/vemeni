using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Vemini
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }
        //Redirecting to login page 
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            //DependencyService.Get<Toast>().LongAlert("Das ist ein Test Toast");
            await Navigation.PushAsync(new Anmeldung());
        }
    }
}
