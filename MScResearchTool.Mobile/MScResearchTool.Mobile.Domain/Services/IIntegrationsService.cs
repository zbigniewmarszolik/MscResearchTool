using MScResearchTool.Mobile.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Domain.Services
{
    public interface IIntegrationsService
    {
        Action<string> ConnectionErrorAction { get; set; }
        Task<IntegrationDistribution> GetIntegrationAsync();
    }
}
