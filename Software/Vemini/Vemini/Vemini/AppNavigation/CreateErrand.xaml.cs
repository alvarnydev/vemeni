using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        /*
         * Author: Benedikt Blank
         * Implemented (last): 29.06.20
         * Neuer Auftrag wird anhand der vom User eingegebenen Daten erstellt und in Datenbank geladen
         */
        private async void Button_OnClickedErstellen(object sender, EventArgs e)
        {
           Errand newErrand = new Errand();

           // User user = StaticUser.ToNonStatic();

           //Access input data from CreateErrand.xaml form
            newErrand.User = Convert.ToInt32(StaticUser.Id);
            newErrand.Type = getPayment();
            newErrand.Category = category_pick.SelectedItem.ToString();
            newErrand.Title = title_entry.Text;
            newErrand.Description = description_editor.Text;
            newErrand.AdresseStreet = street_entry.Text;
            newErrand.AdresseNmbr =streetnumber_entry.Text;
            newErrand.AddressePlz = plz_entry.Text;
            newErrand.AdresseCity = city_entry.Text;
            newErrand.Date = DateTime.Now;
            newErrand.Status = 0;
     

            try
            {
                 var respTask = await ErrandService.AddErrand(newErrand);

                 if (respTask.IsSuccessStatusCode)
                 {
                    DisplayAlert("Bestätigung","Auftrag wurde erstellt.", "Ok");
                 }
            }
            catch (Exception error)
            {
                Console.WriteLine("Auftrag konnte nicht erstellt und in Datenbank geladen werden: "+ error);
            }
        }

        //Checkbox wird in int konvertiert und zurueckgegeben
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

        //Kategorie wird in int konvertiert und zurueckgegeben
        //private int getCategory()
        //{
        //    int category;
        //    switch (category_pick.SelectedItem)
        //    {
        //        case "Supermarkt":
        //            category = 1;
        //            break;
        //        case "Baumarkt":
        //            category = 2;
        //            break;
        //        case "Haushaltshilfe":
        //            category = 3;
        //            break;
        //        case "Tragehilfe":
        //            category = 4;
        //            break;
        //        case "Sonstiges":
        //            category = 5;
        //            break;
        //        default:
        //            category = 0;
        //            break;
        //    }
        //    return category;
        //}
    }
}