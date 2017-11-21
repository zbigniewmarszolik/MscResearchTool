using MScResearchTool.Server.Core.Businesses;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Strategies.DeleteStrategy
{
    public class IntegrationDeleteStrategy : IDeleteStrategy
    {
        private IIntegrationsBusiness _integrationsBusiness { get; set; }

        public IntegrationDeleteStrategy(IIntegrationsBusiness integrationsBusiness)
        {
            _integrationsBusiness = integrationsBusiness;
        }

        public async Task DeleteAsync(int deleteId)
        {
            await _integrationsBusiness.CascadeDeleteAsync(deleteId);
        }
    }
}
