using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.BusinessTests;
using MScResearchTool.Server.Tests.Core.Units;
using System.Collections.Generic;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class TaskInfoBusinessTests : TaskInfoBusinessTestsBase
    {
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

            var mockCrackingsBusiness = testingUnit.GetDependency<Mock<ICrackingsBusiness>>();
            mockCrackingsBusiness.Setup(av => av.ReadAvailableAsync()).ReturnsAsync(new List<Cracking>()
            {
                new Cracking(),
                new Cracking()
            });

            ITaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetFullTasksAvailabilityAsync();

            Assert.True(result.IsIntegrationAvailable);
            Assert.True(result.IsCrackingAvailable);
        }

        [Fact]
        public async void GetFullTasksAvailabilityAsync_EmptyInput_ReturnsTasksUnavailability()
        {
            TestingUnit<TaskInfoBusiness> testingUnit = GetUnit();

            var mockIntegrationsBusiness = testingUnit.GetDependency<Mock<IIntegrationsBusiness>>();
            mockIntegrationsBusiness.Setup(av => av.ReadAvailableAsync()).ReturnsAsync(new List<Integration>());

            var mockCrackingsBusiness = testingUnit.GetDependency<Mock<ICrackingsBusiness>>();
            mockCrackingsBusiness.Setup(av => av.ReadAvailableAsync()).ReturnsAsync(new List<Cracking>());

            ITaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetFullTasksAvailabilityAsync();

            Assert.False(result.IsIntegrationAvailable);
            Assert.False(result.IsCrackingAvailable);
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

            var mockCrackingDistsBusiness = testingUnit.GetDependency<Mock<ICrackingDistributionsBusiness>>();
            mockCrackingDistsBusiness.Setup(avd => avd.ReadAvailableAsync()).ReturnsAsync(new List<CrackingDistribution>()
            {
                new CrackingDistribution(),
                new CrackingDistribution(),
                new CrackingDistribution(),
                new CrackingDistribution()
            });

            ITaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetDistributedTasksAvailabilityAsync();

            Assert.True(result.IsIntegrationAvailable);
            Assert.True(result.IsCrackingAvailable);
        }

        [Fact]
        public async void GetDistributedTasksAvailabilityAsync_EmptyInput_ReturnsTasksUnavailability()
        {
            TestingUnit<TaskInfoBusiness> testingUnit = GetUnit();

            var mockIntegrationDistsBusiness = testingUnit.GetDependency<Mock<IIntegrationDistributionsBusiness>>();
            mockIntegrationDistsBusiness.Setup(avd => avd.ReadAvailableAsync()).ReturnsAsync(new List<IntegrationDistribution>());

            var mockCrackingDistsBusiness = testingUnit.GetDependency<Mock<ICrackingDistributionsBusiness>>();
            mockCrackingDistsBusiness.Setup(avd => avd.ReadAvailableAsync()).ReturnsAsync(new List<CrackingDistribution>());

            ITaskInfoBusiness taskInfoBusiness = testingUnit.GetResolvedTestingUnit();

            var result = await taskInfoBusiness.GetDistributedTasksAvailabilityAsync();

            Assert.False(result.IsIntegrationAvailable);
            Assert.False(result.IsCrackingAvailable);
        }
    }
}
