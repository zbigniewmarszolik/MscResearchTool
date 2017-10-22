using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Factories
{
    public class IntegrationDistributionFactory
    {
        public IntegrationDistribution GetInstance(Integration task, double downLimit, double upLimit)
        {
            var instance = new IntegrationDistribution()
            {
                Accuracy = task.Accuracy / task.DroidIntervals,
                CreationDate = task.CreationDate,
                DownBoundary = downLimit,
                UpBoundary = upLimit,
                Formula = task.Formula,
                IsTrapezoidMethodRequested = task.IsTrapezoidMethodRequested,
                Task = task,
                IsAvailable = true,
                IsFinished = false,
                DeviceRAM = 0,
                DeviceCPU = "Unknown"
            };

            return instance;
        }
    }
}
