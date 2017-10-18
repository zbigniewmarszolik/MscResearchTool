using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Core.Factories
{
    public interface IIntegrationDistributionFactory
    {
        IntegrationDistribution GetInstance(IntegrationTask task, double downLimit, double upLimit);
    }
}
