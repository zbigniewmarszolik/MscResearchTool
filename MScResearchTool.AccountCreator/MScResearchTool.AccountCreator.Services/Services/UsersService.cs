using System.Threading.Tasks;
using MScResearchTool.AccountCreator.Domain.Models;
using MScResearchTool.AccountCreator.Domain.Services;
using MScResearchTool.AccountCreator.Services.Factories;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace MScResearchTool.AccountCreator.Services.Services
{
    public class UsersService : ServiceBase, IUsersService
    {
        public UsersService(HttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.GetInstance();
        }

        public async Task PostUserAsync(User userToPost)
        {
            var directUrl = ServerUrl + "CreateUser";

            var uri = new Uri(string.Format(directUrl));

            var json = JsonConvert.SerializeObject(userToPost);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await Client.PostAsync(uri, content).ContinueWith((x) =>
            {
                try
                {
                    x.Result.EnsureSuccessStatusCode();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            });
        }
    }
}
