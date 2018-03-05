using System.Collections.Generic;
using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class CrackingDistributionsBusiness : ICrackingDistributionsBusiness
    {
        public Task<IList<CrackingDistribution>> ReadAllEagerAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<CrackingDistribution>> ReadAvailableAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<CrackingDistribution> ReadByIdAsync(int distributionId)
        {
            throw new System.NotImplementedException();
        }

        public Task UnstuckByIdAsync(int crackingId)
        {
            throw new System.NotImplementedException();
        }

        public Task UnstuckTakenAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(CrackingDistribution crackingDistribution)
        {
            throw new System.NotImplementedException();
        }
    }
}
