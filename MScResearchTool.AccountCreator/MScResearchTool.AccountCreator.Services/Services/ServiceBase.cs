using System.Net.Http;

namespace MScResearchTool.AccountCreator.Services.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient Client { get; set; }
        protected static string ServerUrl = "http://192.168.0.102//Api/";
    }
}
