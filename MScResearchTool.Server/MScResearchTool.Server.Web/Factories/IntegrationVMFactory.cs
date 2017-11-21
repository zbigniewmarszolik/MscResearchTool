using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Web.Converters;
using MScResearchTool.Server.Web.ViewModels;

namespace MScResearchTool.Server.Web.Factories
{
    public class IntegrationVMFactory
    {
        private TaskTypeConverter _taskTypeConverter { get; set; }

        public IntegrationVMFactory(TaskTypeConverter taskTypeConverter)
        {
            _taskTypeConverter = taskTypeConverter;
        }

        public IntegrationViewModel GetInstance()
        {
            var instance = new IntegrationViewModel()
            {
                Formula = "x*sin(x)",
                Precision = 1000,
                UpperLimit = "100",
                LowerLimit = "0",
                IntervalsCount = 2,
                Method = _taskTypeConverter.EnumeratorToString(ETaskType.SquareIntegration)
            };

            return instance;
        }
    }
}
