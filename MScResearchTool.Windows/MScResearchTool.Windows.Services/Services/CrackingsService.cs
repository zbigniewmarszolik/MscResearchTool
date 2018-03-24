using System;
using System.Net.Http;
using System.Threading.Tasks;
using MScResearchTool.Windows.Domain.Models;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Services.Factories;
using Newtonsoft.Json;

namespace MScResearchTool.Windows.Services.Services
{
    public class CrackingsService : ServiceBase, ICrackingsService
    {
        public Action<string> ConnectionErrorAction { get; set; }

        public CrackingsService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task<Cracking> GetCrackingAsync()
        {
            var directUrl = ServerUrl + "GetCracking" + ClientMode;

            var uri = new Uri(string.Format(directUrl));

            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(uri);

                if(responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var cracking = JsonConvert.DeserializeObject<Cracking>(responseData);

                    return cracking;
                }
            }
            catch(Exception e)
            {
                ConnectionErrorAction("Error connecting to the server for getting cracking task to break password with following exception: " + "\n" + e.Message);
            }

            return null;
        }
    }
}
