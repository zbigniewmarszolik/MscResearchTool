using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IIntegrationDistributionsBusiness
    {
        Task<IList<IntegrationDistribution>> ReadAvailableIntegrationDistributionsAsync();
        Task UpdateIntegrationDistributionAsync(IntegrationDistribution integrationDistribution);
        Task<IntegrationDistribution> ReadByIdAsync(int distributionId);
        Task UnstuckTakenDistributionsAsync();
    }
}
