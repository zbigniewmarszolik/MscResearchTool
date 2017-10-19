using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IIntegrationDistributionsBusiness
    {
        Task<IList<IntegrationDistribution>> ReadAvailableAsync();
        Task<IntegrationDistribution> ReadByIdAsync(int distributionId);
        Task UpdateAsync(IntegrationDistribution integrationDistribution);
        Task UnstuckTakenAsync();
    }
}
