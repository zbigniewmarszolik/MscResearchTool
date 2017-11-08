using MScResearchTool.Windows.Domain.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Windows.Domain.Businesses
{
    public interface IIntegrationsBusiness
    {
        Task<IntegrationResult> CalculateIntegrationAsync(Integration integrationTask);
    }
}
