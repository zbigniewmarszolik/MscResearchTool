using System.Net.Http;

namespace MScResearchTool.Windows.Services.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient Client { get; set; }
        protected static string ServerUrl = "http://192.168.0.100//Api/";
        protected static string ClientMode = "/Single";
    }
}
