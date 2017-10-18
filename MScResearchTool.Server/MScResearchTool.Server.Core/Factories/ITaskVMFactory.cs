using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Factories
{
    public interface ITaskVMFactory<TViewModel>
    {
        IList<TViewModel> GetCollection(IList<IntegrationTask> integrations);
    }
}
