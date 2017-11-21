using MScResearchTool.Mobile.Domain.Models;
using MScResearchTool.Mobile.Domain.Services;
using MScResearchTool.Mobile.Services.Factories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Services.Services
{
    public class IntegrationsService : ServiceBase, IIntegrationsService
    {
        public Action<string> ConnectionErrorAction { get; set; }

        public IntegrationsService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task<IntegrationDistribution> GetIntegrationAsync()
        {
            var directUrl = ServerUrl + "GetIntegration" + ClientMode;

            var uri = new Uri(string.Format(directUrl));

            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var integration = JsonConvert.DeserializeObject<IntegrationDistribution>(responseData);

                    return integration;
                }
            }
            catch (Exception ex)
            {
                ConnectionErrorAction("Error connecting to the server for getting integration task to calculate.");

                throw ex;
            }

            return null;
        }
    }
}
