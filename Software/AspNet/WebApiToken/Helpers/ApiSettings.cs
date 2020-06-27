
namespace WebApiToken.Helpers
{
    public class ApiSettings
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
