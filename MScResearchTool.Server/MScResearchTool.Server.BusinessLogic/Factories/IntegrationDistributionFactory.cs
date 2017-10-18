using MScResearchTool.Server.Core.Factories;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Factories
{
    public class IntegrationDistributionFactory : IIntegrationDistributionFactory
    {
        public IntegrationDistribution GetInstance(IntegrationTask task, double downLimit, double upLimit)
        {
            var instance = new IntegrationDistribution()
            {
                Accuracy = task.Accuracy/task.DroidIntervals,
                CreationDate = task.CreationDate,
                DownBoundary = downLimit,
                UpBoundary = upLimit,
                Formula = task.Formula,
                IsTrapezoidMethodRequested = task.IsTrapezoidMethodRequested,
                Task = task
            };

            return instance;
        }
    }
}
