using System;
using System.Net.Http;
using System.Threading.Tasks;
using MScResearchTool.Mobile.Domain.Models;
using MScResearchTool.Mobile.Domain.Services;
using MScResearchTool.Mobile.Services.Factories;
using Newtonsoft.Json;

namespace MScResearchTool.Mobile.Services.Services
{
    public class CrackingsService : ServiceBase, ICrackingsService
    {
        public CrackingsService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task<CrackingDistribution> GetCrackingAsync()
        {
            var directUrl = ServerUrl + "GetCracking" + ClientMode;

            var uri = new Uri(string.Format(directUrl));

            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var cracking = JsonConvert.DeserializeObject<CrackingDistribution>(responseData);

                    return cracking;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
