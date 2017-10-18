using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Repositories
{
    public interface IIntegrationDistributionsRepository
    {
        void Create(IntegrationDistribution integrationDistribution);
        IList<IntegrationDistribution> Read();
        void Delete(int integrationDistributionId);
    }
}
