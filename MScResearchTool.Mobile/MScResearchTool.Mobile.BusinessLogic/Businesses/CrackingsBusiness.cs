using System.Threading.Tasks;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Domain.Models;

namespace MScResearchTool.Mobile.BusinessLogic.Businesses
{
    public class CrackingsBusiness : ICrackingsBusiness
    {
        public async Task<CrackingResult> AttemptPasswordBreakingPasswordAsync(CrackingDistribution crackingDistributionTask) // if will not find, return NULL, it is handled in further steps by NULL
        {
            throw new System.NotImplementedException();
        }
    }
}
