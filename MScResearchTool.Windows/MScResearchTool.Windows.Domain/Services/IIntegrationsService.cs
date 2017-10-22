using MScResearchTool.Windows.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Windows.Domain.Services
{
    public interface IIntegrationsService
    {
        Action<string> ConnectionErrorAction { get; set; }
        Task<Integration> GetIntegrationAsync();
    }
}
