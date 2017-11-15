using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.Units;
using System.Collections.Generic;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class TaskInfoBusinessTests
    {
        private TestingUnit<TaskInfoBusiness> GetUnit()
        {
            var unit = new TestingUnit<TaskInfoBusiness>();

            unit.AddDependency(new Mock<IIntegrationsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IIntegrationDistributionsBusiness>(MockBehavior.Strict));

            return unit;
        }

        [Fact]
        public async void GetFullTasksAvailabilityAsync_NotEmptyInput_ReturnsTasksAvailability()
        {
            TestingUnit<TaskInfoBusiness> testingUnit = GetUnit();

            var mockIntegrationsBusiness = testingUnit.GetDependency<Mock<IIntegrationsBusiness>>();
            mockIntegrationsBusiness.Setup(av => av.ReadAvailableAsync()).ReturnsAsync(new List<Integration>()
            {
                new Integration(),
                new Integration()
            });

            TaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetFullTasksAvailabilityAsync();

            Assert.True(result.IsIntegrationAvailable);
        }

        [Fact]
        public async void GetFullTasksAvailabilityAsync_EmptyInput_ReturnsTasksUnavailability()
        {
            TestingUnit<TaskInfoBusiness> testingUnit = GetUnit();

            var mockIntegrationsBusiness = testingUnit.GetDependency<Mock<IIntegrationsBusiness>>();
            mockIntegrationsBusiness.Setup(av => av.ReadAvailableAsync()).ReturnsAsync(new List<Integration>());

            TaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetFullTasksAvailabilityAsync();

            Assert.False(result.IsIntegrationAvailable);
        }

        [Fact]
        public async void GetDistributedTasksAvailabilityAsync_NotEmptyInput_ReturnsTasksAvailability()
        {
            TestingUnit<TaskInfoBusiness> testingUnit = GetUnit();

            var mockIntegrationDistsBusiness = testingUnit.GetDependency<Mock<IIntegrationDistributionsBusiness>>();
            mockIntegrationDistsBusiness.Setup(avd => avd.ReadAvailableAsync()).ReturnsAsync(new List<IntegrationDistribution>()
            {
                new IntegrationDistribution(),
                new IntegrationDistribution(),
                new IntegrationDistribution(),
                new IntegrationDistribution()
            });

            TaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetDistributedTasksAvailabilityAsync();

            Assert.True(result.IsIntegrationAvailable);
        }

        [Fact]
        public async void GetDistributedTasksAvailabilityAsync_EmptyInput_ReturnsTasksUnavailability()
        {
            TestingUnit<TaskInfoBusiness> testingUnit = GetUnit();

            var mockIntegrationDistsBusiness = testingUnit.GetDependency<Mock<IIntegrationDistributionsBusiness>>();
            mockIntegrationDistsBusiness.Setup(avd => avd.ReadAvailableAsync()).ReturnsAsync(new List<IntegrationDistribution>());

            TaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetDistributedTasksAvailabilityAsync();

            Assert.False(result.IsIntegrationAvailable);
        }
    }
}
