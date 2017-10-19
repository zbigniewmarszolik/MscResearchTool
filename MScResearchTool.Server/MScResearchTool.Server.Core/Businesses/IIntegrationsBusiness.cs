using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IIntegrationsBusiness
    {
        Task DistributeAndPersistAsync(Integration integrationTask);
        Task<IList<Integration>> ReadAllAsync();
        Task<IList<Integration>> ReadAllEagerAsync();
        Task<IList<Integration>> ReadAvailableAsync();
        Task<Integration> ReadByIdAsync(int taskId);
        Task UpdateAsync(Integration integrationTask);
        Task UnstuckTakenAsync();
        Task CascadeDeleteAsync(int taskId);
    }
}
