using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Core.Factories
{
    public interface IIntegrationDistributionFactory
    {
        IntegrationDistribution GetInstance(Integration task, double downLimit, double upLimit);
    }
}
