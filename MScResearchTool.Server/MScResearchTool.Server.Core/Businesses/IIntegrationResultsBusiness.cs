using MScResearchTool.Server.Core.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IIntegrationResultsBusiness
    {
        Task ProcessResultAsync(IntegrationResult result);
    }
}
