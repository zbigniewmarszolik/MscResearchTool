using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Factories
{
    public interface IViewModelFactory<TViewModel>
    {
        TViewModel GetInstance();
        IList<TViewModel> GetCollection(IList<Integration> integrations);
    }
}
