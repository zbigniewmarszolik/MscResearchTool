using System.Threading.Tasks;
using MScResearchTool.Windows.Domain.Models;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Services.Factories;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MScResearchTool.Windows.Services.Services
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
