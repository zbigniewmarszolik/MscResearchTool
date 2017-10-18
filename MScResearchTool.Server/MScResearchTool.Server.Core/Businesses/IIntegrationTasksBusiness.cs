using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IIntegrationTasksBusiness
    {
        Task DistributeAndPersistAsync(IntegrationTask integrationTask);
        Task<IList<IntegrationTask>> ReadAllIntegrationTasksAsync();
        Task<IList<IntegrationTask>> ReadAllIntegrationTasksEagerAsync();
        Task CascadeDeleteAsync(int taskId);
        Task<IList<IntegrationTask>> ReadAvailableFullIntegrationsAsync();
        Task UpdateIntegrationTaskAsync(IntegrationTask integrationTask);
        Task<IntegrationTask> ReadByIdAsync(int taskId);
        Task UnstuckTakenTasksAsync();
    }
}
