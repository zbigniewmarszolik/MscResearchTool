using System;
using System.Net.Http;

namespace MScResearchTool.AccountCreator.Services.Factories
{
    public class HttpClientFactory
    {
        public HttpClient GetInstance()
        {
            var instance = new HttpClient
            {
                MaxResponseContentBufferSize = 256000,
                Timeout = new TimeSpan(0, 0, 15)
            };

            return instance;
        }
    }
}
