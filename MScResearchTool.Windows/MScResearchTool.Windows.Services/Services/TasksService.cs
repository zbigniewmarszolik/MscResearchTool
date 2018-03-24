using System.Threading.Tasks;
using MScResearchTool.Windows.Domain.Models;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Services.Factories;
using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace MScResearchTool.Windows.Services.Services
{
    public class TasksService : ServiceBase, ITasksService
    {
        public Action<string> ConnectionErrorAction { get; set; }

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
            catch(Exception e)
            {
                ConnectionErrorAction("Error connecting to the server for reading available tasks with following exception: " + "\n" + e.Message);
            }

            return null;
        }
    }
}
