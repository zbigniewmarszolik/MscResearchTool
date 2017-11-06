using System.Net.Http;

namespace MScResearchTool.Mobile.Services.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient Client { get; set; }
        protected static string ServerUrl = "http://192.168.0.101//Api/";
        protected static string ClientMode = "/Distributed";
    }
}
