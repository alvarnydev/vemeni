using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Vemin;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Vemini.Services
{
    public class Service
    {
        public Service()
        {
        }
        public async void GetUserInfoById(int id)
        {

            string url = "https://vergissmeinnicht.f2.htw-berlin.de/api/users/" + id;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = client.SendAsync(request).Result;
            using (HttpContent content = response.Content)
            {

                var json = content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(json.Result);
                Console.WriteLine(user);

                Application.Current.Properties.Clear();
                Application.Current.Properties.Add("Id", user.Id);
                Application.Current.Properties.Add("UserName", user.UserName);
                Application.Current.Properties.Add("EmailAddress", user.Email);
                Application.Current.Properties.Add("City", user.City);
                Application.Current.Properties.Add("Street", user.Street);
                

                Application.Current.Properties.Add("ProfilePicture", user.Picture);
            }
           
        }

        public async void UpdateUser()
        {
            User u = new User("4","d.sawckin@yandex.ru","Dmitry Savkin", "", "","Berlin","10000", "Strasse", "","");
            string url = "https://vergissmeinnicht.f2.htw-berlin.de/api/users/4";
            var client = new HttpClient();
            var s = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(u,s);
            Console.WriteLine(json);
            /* var content = new StringContent(json, Encoding.UTF8, "application/json");
             var response =  await client.PutAsync(url, content);
         */
            string jsonstr = @"{""id"":4,""username"":""Dmitry Savkin"",""phone"":""1111111"",""email"":""dsawckin@yandex.ru"",""address_plz"":0,""address_city"":""Kalrsruhe"",""address_street"":""TBD"",""address_strnmbr"":0,""img"":""0"",""lastvisit"":""2020-06-15T17:51:05"",""created"":""2020-06-01T13:49:01""}";
            var content = new StringContent(jsonstr, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(url, content);
            Console.WriteLine("Result " + response);
            Console.WriteLine("");
        }
    }
}
