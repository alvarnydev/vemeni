using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Vemini.Models;


namespace Vemini.Services

{
    public class ErrandService
    {
        public void AddErrand(Errand errand)
        {
            string url = "https://vergissmeinnicht.f2.htw-berlin.de/api/jobs";
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
            httpWebRequest.Method = "PUT";
        }
    }
}