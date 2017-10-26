using System.Net.Http;

namespace MScResearchTool.Mobile.Services.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient Client { get; set; }
        protected static string ServerUrl = "http://localhost:61048//Api/";
        protected static string ClientMode = "/Distributed";
    }
}
