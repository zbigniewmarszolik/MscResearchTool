using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IIntegrationTasksBusiness
    {
        Task DistributeAndPersist(IntegrationTask integrationTask);
        Task<IList<IntegrationTask>> ReadAllIntegrationTasks();
        Task CascadeDelete(int taskId);
    }
}
