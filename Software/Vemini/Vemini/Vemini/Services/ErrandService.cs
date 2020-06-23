using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Vemini.Models;
using Xamarin.Forms;

namespace Vemini.Services

{
    public static class ErrandService
    {
        public async static void AddErrand(Errand errand)
        {
            string url = "https://vergissmeinnicht.f2.htw-berlin.de/api/jobs";
            var client = new HttpClient();
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(errand, settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);

            DependencyService.Get<Toast>().LongAlert("Result: " + response);
        }
    }
}