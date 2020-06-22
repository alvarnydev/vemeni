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
            UserRefactored u = new UserRefactored("4", "uoeaz@student.kit.edu", "33443", "Dmitry Savkin", "", "Karlsruhe", "76543", "Werderstr", "2020-06-01T13:49:01", "2020-06-01T13:49:01");
            string url = "https://vergissmeinnicht.f2.htw-berlin.de/api/users/4";
            var client = new HttpClient();
            var s = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(u,s);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(url, content);
            Console.WriteLine("Result " + response);
            Console.WriteLine("");
        }
    }
}
