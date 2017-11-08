using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IReportsBusiness
    {
        Task GenerateIntegrationReportAsync(int fullIntegrationid);
        Task<IList<Report>> ReadAllAsync();
        Task<Report> ReadByIdAsync(int reportId);
        Task DeleteAsync(int reportId);
    }
}
