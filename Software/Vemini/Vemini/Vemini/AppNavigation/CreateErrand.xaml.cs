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
	public partial class CreateErrand : ContentPage
	{
		public CreateErrand ()
		{
			InitializeComponent ();
		}

        private void Button_OnClickedErstellen(object sender, EventArgs e)
        {
           Errand newErrand = new Errand();
            // newErrand.User = Getcurrent User 


            newErrand.Type = getPayment();
            newErrand.Category = getCategory();
            newErrand.Title = title_entry.Text;
            newErrand.Description = description_editor.Text;
           // newErrand.LocationLat = street_entry.Text;
            newErrand.LocationLon = Convert.ToDouble(streetnumber_entry.Text);
            newErrand.Address = plz_entry.Text;
            // newErrand.city = city_entry.Text;
            newErrand.Date = DateTime.Now;
            newErrand.Status = 0;
            newErrand.AcceptedBy = 0;
        }

        private int getPayment()
        {
            bool payment;

            if (payment_cB.IsChecked)
            {
                payment = true;
            }
            else
            {
                payment = false;
            }

            return Convert.ToInt32(payment);
        }

        private int getCategory()
        {
            int category;
            switch (category_pick.SelectedItem)
            {
                case "Supermarkt":
                    category = 1;
                    break;
                case "Baumarkt":
                    category = 2;
                    break;
                case "Haushaltshilfe":
                    category = 3;
                    break;
                case "Tragehilfe":
                    category = 4;
                    break;
                case "Sonstiges":
                    category = 5;
                    break;
                default:
                    category = 0;
                    break;
            }
            return category;
        }
    }
}