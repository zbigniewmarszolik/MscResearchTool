using MScResearchTool.Server.Core.Businesses;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Strategies.UnstuckStrategy
{
    public class CrackingUnstuckStrategy : IUnstuckStrategy
    {
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        private ICrackingDistributionsBusiness _crackingDistributionsBusiness { get; set; }

        public CrackingUnstuckStrategy(ICrackingsBusiness crackingsBusiness, ICrackingDistributionsBusiness crackingDistributionsBusiness)
        {
            _crackingsBusiness = crackingsBusiness;
            _crackingDistributionsBusiness = crackingDistributionsBusiness;
        }

        public async Task UnstuckAsync(int unstuckId)
        {
            await _crackingDistributionsBusiness.UnstuckByIdAsync(unstuckId);
            await _crackingsBusiness.UnstuckByIdAsync(unstuckId);
        }
    }
}
