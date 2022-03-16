using Newtonsoft.Json;

namespace Vemini
{
    class UserLogin
    {

        [JsonProperty("username")] 
        public string EmailOrUsername { get; set; }

        [JsonProperty("password")] 
        public string Password { get; set; }

    }
}
