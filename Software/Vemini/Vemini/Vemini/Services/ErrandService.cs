using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Vemini.Models;
using Xamarin.Forms;

/*
* Author: Benedikt Blank
* First Implemented: 15.06.20
* Funktionen für das Posten und Getten von Auftraegen aus der Datenbank
*/

namespace Vemini

{
    public static class ErrandService
    {
        //Funktion um Auftrag in Datenbank zu stellen (Posten)
        //Parameter: Hochzuladener Auftrag
        
        public async static void AddErrand(Errand errand)
        {
            string url = Constants.VeminiJobsUrl;
            var client = new HttpClient();
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(errand, settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
        }

        //Funktion um Auftraege aus Datenbank zu bekommen (Get)
        //Parameter: Stadt aus der die Auftraege genommen werden sollen
        //Rueckgabe: Liste mit allen Auftraegen aus dieser Stadt

        public static List<Errand> GetErrands(string city)
        {
            string url = Constants.VeminiJobsCityUrl + city;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = client.SendAsync(request).Result;
            using (HttpContent content = response.Content)
            {

                var json = content.ReadAsStringAsync();
                List<Errand> DbErrands = JsonConvert.DeserializeObject <List<Errand>>(json.Result);
                return DbErrands;
                
            }
        }
    }

}