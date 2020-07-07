using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vemini.Models;

namespace Vemini.Services
{
    class MessageService
    {

        private static readonly HttpClient client = new HttpClient();


        // Gets chats to user id
        public async Task<IEnumerable<Chat>> GetChatsToUser(int id)
        {

            // Get url
            string url = $"https://vergissmeinnicht.f2.htw-berlin.de/api/chats/user/{id}";

            // Retrieve token from static user
            var token = StaticUser.Token;

            // Pass token to clients headers
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Make get request
            HttpResponseMessage response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            // Create chat object
            var chats = new List<Chat>();

            // Extract chat ids from request
            if (response.IsSuccessStatusCode)
            {
                // Deserialize
                chats = JsonConvert.DeserializeObject<List<Chat>>(responseString);

                // Print stuff

            }
            else
            {
                throw new Exception("blub");
            }

            return chats;

        }
    }
}
