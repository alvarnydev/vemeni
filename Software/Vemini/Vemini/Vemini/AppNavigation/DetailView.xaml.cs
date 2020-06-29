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
	public partial class DetailView : ContentPage
	{
		public DetailView (string title, string description, string plz, string street, string number ,string city)
        {
            InitializeComponent ();

            title_label.Text = title;
            description_label.Text = description;
            street_label.Text = street + " " + number;
            plz_label.Text = plz + " " + city;
        }
	}
}