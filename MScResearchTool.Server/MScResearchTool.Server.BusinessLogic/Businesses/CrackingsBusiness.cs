using System.Collections.Generic;
using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class CrackingsBusiness : ICrackingsBusiness
    {
        public Task CascadeDeleteAsync(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public Task DistributeAndPersistAsync(Cracking crackingTask)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Cracking>> ReadAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Cracking>> ReadAllEagerAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Cracking>> ReadAvailableAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Cracking> ReadByIdAsync(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public Task UnstuckByIdAsync(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public Task UnstuckTakenAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Cracking integrationTask)
        {
            throw new System.NotImplementedException();
        }
    }
}
