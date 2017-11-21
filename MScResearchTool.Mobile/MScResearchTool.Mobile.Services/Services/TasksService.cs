using MScResearchTool.Mobile.Domain.Models;
using MScResearchTool.Mobile.Domain.Services;
using MScResearchTool.Mobile.Services.Factories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Services.Services
{
    public class TasksService : ServiceBase, ITasksService
    {
        public TasksService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task<TaskInfo> GetTasksAvailabilityAsync()
        {
            var directUrl = ServerUrl + "CheckTasksAvailability" + ClientMode;

            var uri = new Uri(string.Format(directUrl));

            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var taskInfo = JsonConvert.DeserializeObject<TaskInfo>(responseData);

                    return taskInfo;
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
