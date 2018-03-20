using Moq;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using MScResearchTool.Server.Web.Converters;
using MScResearchTool.Server.Web.Helpers;

namespace MScResearchTool.Server.Tests.Core.HelperTests
{
    public abstract class IntegralInitializationHelperTestsBase : MockBase<TestingUnit<IntegralInitializationHelper>>
    {
        protected override TestingUnit<IntegralInitializationHelper> GetUnit()
        {
            var unit = new TestingUnit<IntegralInitializationHelper>();

            unit.AddDependency(new Mock<TaskTypeConverter>(MockBehavior.Strict));

            return unit;
        }

        protected IntegralInitializationHelper GetUniversalMockedUnit()
        {
            TestingUnit<IntegralInitializationHelper> testingUnit = GetUnit();

            var mockTaskTypeConverter = testingUnit.GetDependency<Mock<TaskTypeConverter>>();
            mockTaskTypeConverter.Setup(ets => ets.EnumeratorToString(ETaskType.SquareIntegration)).CallBase();
            mockTaskTypeConverter.Setup(ets => ets.EnumeratorToString(ETaskType.TrapezoidIntegration)).CallBase();

            return testingUnit.GetResolvedTestingUnit();
        }
    }
}
