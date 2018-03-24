using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MScResearchTool.Mobile.Domain.Models;
using MScResearchTool.Mobile.Domain.Services;
using MScResearchTool.Mobile.Services.Factories;
using Newtonsoft.Json;

namespace MScResearchTool.Mobile.Services.Services
{
    public class CrackingResultsService : ServiceBase, ICrackingResultsService
    {
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
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
