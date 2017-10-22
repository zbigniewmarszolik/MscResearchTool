using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.ViewModels;

namespace MScResearchTool.Server.Web.Factories
{
    public class IntegrationVMFactory
    {
        public IntegrationViewModel GetInstance()
        {
            var instance = new IntegrationViewModel()
            {
                Formula = "x*sin(x)",
                Precision = 1000,
                UpperLimit = "100",
                LowerLimit = "0",
                IntervalsCount = 2
            };

            return instance;
        }

        public IList<IntegrationViewModel> GetCollection(IList<Integration> integrations)
        {
            return new List<IntegrationViewModel>();
        }
    }
}
