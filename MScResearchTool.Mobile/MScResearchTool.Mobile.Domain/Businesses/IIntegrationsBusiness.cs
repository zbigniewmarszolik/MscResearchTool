using MScResearchTool.Mobile.Domain.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Domain.Businesses
{
    public interface IIntegrationsBusiness
    {
        Task<IntegrationResult> CalculateIntegrationAsync(IntegrationDistribution integrationDistributionTask);
    }
}
