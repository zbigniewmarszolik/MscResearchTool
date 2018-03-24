using MScResearchTool.Mobile.Domain.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Domain.Businesses
{
    public interface ICrackingsBusiness
    {
        Task<CrackingResult> AttemptPasswordBreakingPasswordAsync(CrackingDistribution crackingDistributionTask);
    }
}
