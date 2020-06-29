using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Vemini.Models;
using Xamarin.Forms;


namespace Vemini

{
    public static class ErrandService
    {
        public async static void AddErrand(Errand errand)
        {
            string url = Constants.VeminiJobsUrl;
            var client = new HttpClient();
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(errand, settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);

           //DependencyService.Get<Toast>().LongAlert("Result: " + response);
        }

        public async static void GetErrands(string city)
        {
            string url = Constants.VeminiJobsCityUrl + city;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = client.SendAsync(request).Result;
            using (HttpContent content = response.Content)
            {

                var json = content.ReadAsStringAsync();
                Errand DbErrand = JsonConvert.DeserializeObject<Errand>(json.Result);
            }
        }
    }

}