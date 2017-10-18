using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Repositories
{
    public interface IIntegrationTasksRepository
    {
        void Create(IntegrationTask integrationTask);
        IList<IntegrationTask> Read();
        void Update(IntegrationTask integrationTask);
        void Delete(int integrationTaskId);
    }
}
