using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface ICrackingDistributionsBusiness
    {
        Task<IList<CrackingDistribution>> ReadAllEagerAsync();
        Task<IList<CrackingDistribution>> ReadAvailableAsync();
        Task<CrackingDistribution> ReadByIdAsync(int distributionId);
        Task UpdateAsync(CrackingDistribution crackingDistribution);
        Task UnstuckTakenAsync();
        Task UnstuckByIdAsync(int crackingId);
    }
}
