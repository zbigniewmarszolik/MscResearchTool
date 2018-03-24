using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MScResearchTool.Windows.Domain.Models;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Services.Factories;
using Newtonsoft.Json;

namespace MScResearchTool.Windows.Services.Services
{
    public class CrackingResultsService : ServiceBase, ICrackingResultsService
    {
        public Action<string> ConnectionErrorAction { get; set; }

        public CrackingResultsService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task PostResultAsync(CrackingResult result)
        {
            var directUrl = ServerUrl + "PostCrackingResult" + ClientMode;

            var uri = new Uri(string.Format(directUrl));

            var json = JsonConvert.SerializeObject(result);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await Client.PutAsync(uri, content).ContinueWith((x) =>
            {
                try
                {
                    x.Result.EnsureSuccessStatusCode();
                }
                catch(Exception e)
                {
                    ConnectionErrorAction("Error connecting to the server for posting cracking result with following exception: " + "\n" + e.Message);
                }
            });
        }
    }
}
