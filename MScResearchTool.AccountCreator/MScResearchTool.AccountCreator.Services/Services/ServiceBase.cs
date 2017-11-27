using System.Net.Http;

namespace MScResearchTool.AccountCreator.Services.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient Client { get; set; }
        protected static string ServerUrl = "http://localhost:80//Api/";
    }
}
