using MScResearchTool.Server.Core.Businesses;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Strategies.DeleteStrategy
{
    public class CrackingDeleteStrategy : IDeleteStrategy
    {
        private ICrackingsBusiness _crackingsBusiness { get; set; }

        public CrackingDeleteStrategy(ICrackingsBusiness crackingsBusiness)
        {
            _crackingsBusiness = crackingsBusiness;
        }

        public async Task DeleteAsync(int deleteId)
        {
            await _crackingsBusiness.CascadeDeleteAsync(deleteId);
        }
    }
}
