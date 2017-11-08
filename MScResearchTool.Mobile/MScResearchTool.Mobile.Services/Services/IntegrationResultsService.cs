using MScResearchTool.Mobile.Domain.Models;
using MScResearchTool.Mobile.Domain.Services;
using MScResearchTool.Mobile.Services.Factories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Services.Services
{
    public class IntegrationResultsService : ServiceBase, IIntegrationResultsService
    {
        public Action<string> ConnectionErrorAction { get; set; }

        public IntegrationResultsService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task PostResultAsync(IntegrationResult result)
        {
            var directUrl = ServerUrl + "PostIntegrationResult" + ClientMode;

            var uri = new Uri(string.Format(directUrl));

            var json = JsonConvert.SerializeObject(result);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await Client.PutAsync(uri, content).ContinueWith((x) =>
            {
                try
                {
                    x.Result.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    ConnectionErrorAction("Error connecting to the server for posting integration result.");
                }
            });
        }
    }
}
