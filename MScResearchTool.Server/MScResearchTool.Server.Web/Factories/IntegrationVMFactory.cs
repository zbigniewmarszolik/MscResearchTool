using MScResearchTool.Server.Web.ViewModels;
using MScResearchTool.Server.Core.Types;

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
                IntervalsCount = 2,
                Method = ETaskType.Square_integration.ToString()
            };

            return instance;
        }
    }
}
