using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
                Application.Current.Properties.Add("Id", user.Id);
                Application.Current.Properties.Add("FirstName", user.GivenName);
                Application.Current.Properties.Add("LastName", user.FamilyName);
                Application.Current.Properties.Add("DisplayName", user.UserName);
                Application.Current.Properties.Add("EmailAddress", user.Email);
                Application.Current.Properties.Add("ProfilePicture", user.Picture);
            }
           
        }
    }
}
