using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Vemini.Models;

namespace Vemini.Services

{
    public class ErrandService
    {
        public async void AddErrand(Errand errand)
        {
            string url = "https://vergissmeinnicht.f2.htw-berlin.de/api/jobs";
            var client = new HttpClient();
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(errand, settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(url, content);
        }
    }
}