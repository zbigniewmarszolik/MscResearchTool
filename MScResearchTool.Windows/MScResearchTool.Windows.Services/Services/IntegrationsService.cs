using System.Threading.Tasks;
using MScResearchTool.Windows.Domain.Models;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Services.Factories;
using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace MScResearchTool.Windows.Services.Services
{
    public class IntegrationsService : ServiceBase, IIntegrationsService
    {
        public Action<string> ConnectionErrorAction { get; set; }

        public IntegrationsService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task<Integration> GetIntegrationAsync()
        {
            var directUrl = ServerUrl + "GetIntegration" + ClientMode;

            var uri = new Uri(string.Format(directUrl));

            HttpResponseMessage responseMessage = await Client.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var integration = JsonConvert.DeserializeObject<Integration>(responseData);

                return integration;
            }

            else ConnectionErrorAction("Error connecting to the server for getting integration task to calculate.");

            return null;
        }
    }
}
