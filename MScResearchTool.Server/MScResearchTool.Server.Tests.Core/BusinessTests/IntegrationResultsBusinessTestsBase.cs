using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class IntegrationResultsBusinessTestsBase : MockingTestsBase<TestingUnit<IntegrationResultsBusiness>>
    {
        protected override TestingUnit<IntegrationResultsBusiness> GetUnit()
        {
            var unit = new TestingUnit<IntegrationResultsBusiness>();

            unit.AddDependency(new Mock<IIntegrationsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IIntegrationDistributionsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IReportsBusiness>(MockBehavior.Strict));

            return unit;
        }
    }
}
