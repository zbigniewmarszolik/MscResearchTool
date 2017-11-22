using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class IntegrationsBusinessTestsBase : MockingTestsBase<TestingUnit<IntegrationsBusiness>>
    {
        protected override TestingUnit<IntegrationsBusiness> GetUnit()
        {
            var unit = new TestingUnit<IntegrationsBusiness>();

            unit.AddDependency(new Mock<IIntegrationsRepository>(MockBehavior.Strict));

            return unit;
        }

        protected IntegrationsBusiness GetUniversalMockedUnit()
        {
            return GetUnit().GetResolvedTestingUnit();
        }
    }
}
