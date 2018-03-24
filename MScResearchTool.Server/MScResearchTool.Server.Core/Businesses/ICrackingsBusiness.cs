using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface ICrackingsBusiness
    {
        Task DistributeAndPersistAsync(Cracking crackingTask);
        Task<IList<Cracking>> ReadAllAsync();
        Task<IList<Cracking>> ReadAllEagerAsync();
        Task<IList<Cracking>> ReadAvailableAsync();
        Task<Cracking> ReadByIdAsync(int taskId);
        Task UpdateAsync(Cracking integrationTask);
        Task UnstuckTakenAsync();
        Task UnstuckByIdAsync(int taskId);
        Task CascadeDeleteAsync(int taskId);
    }
}
