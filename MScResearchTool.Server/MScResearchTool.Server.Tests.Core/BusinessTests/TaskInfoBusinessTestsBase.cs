using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class TaskInfoBusinessTestsBase : MockingTestsBase<TestingUnit<TaskInfoBusiness>>
    {
        protected override TestingUnit<TaskInfoBusiness> GetUnit()
        {
            var unit = new TestingUnit<TaskInfoBusiness>();

            unit.AddDependency(new Mock<IIntegrationsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IIntegrationDistributionsBusiness>(MockBehavior.Strict));

            return unit;
        }
    }
}
