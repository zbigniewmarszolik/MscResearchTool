using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Repositories
{
    public interface IIntegrationsRepository
    {
        void Create(Integration integrationTask);
        IList<Integration> Read();
        IList<Integration> ReadEager();
        void Update(Integration integrationTask);
        void Delete(int integrationTaskId);
    }
}
