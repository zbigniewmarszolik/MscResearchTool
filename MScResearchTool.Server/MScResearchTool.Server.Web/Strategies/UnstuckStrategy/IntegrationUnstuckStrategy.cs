using MScResearchTool.Server.Core.Businesses;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Strategies.UnstuckStrategy
{
    public class IntegrationUnstuckStrategy : IUnstuckStrategy
    {
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }

        public IntegrationUnstuckStrategy(
            IIntegrationsBusiness integrationsBusiness, 
            IIntegrationDistributionsBusiness integrationDistributionsBusiness)
        {
            _integrationsBusiness = integrationsBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
        }

        public async Task UnstuckAsync(int unstuckId)
        {
            await _integrationDistributionsBusiness.UnstuckByIdAsync(unstuckId);
            await _integrationsBusiness.UnstuckByIdAsync(unstuckId);
        }
    }
}
