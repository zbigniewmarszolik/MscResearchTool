using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.BusinessTests;
using System;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class IntegrationResultsBusinessTests : IntegrationResultsBusinessTestsBase
    {
        private readonly IntegrationResultsBusiness _testingUnit;

        public IntegrationResultsBusinessTests()
        {
            _testingUnit = GetUniversalMockedUnit();
        }

        [Fact]
        public async void ProcessResultAsync_NullInput_ThrowingException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
             {
                 await _testingUnit.ProcessResultAsync(null);
             });
        }

        [Fact]
        public async void ProcessResultAsync_NanResult_HandlesCorrectly()
        {
            var integrationResult = new IntegrationResult()
            {
                Id = IntegrationId,
                CPU = "cpu example",
                RAM = 2048,
                IsDistributed = false,
                ElapsedSeconds = 11.82,
                IsResultNotANumber = false,
                Result = Double.NaN
            };

            await _testingUnit.ProcessResultAsync(integrationResult);

            Assert.Equal(0.0, integrationResult.Result);
            Assert.True(integrationResult.IsResultNotANumber);
        }

        [Fact]
        public async void ProcessResultAsync_ProperInput_GenerateReport()
        {
            var reportsBeforeProcessing = ReportsDatabase.Count;
            var integrationTasksBeforeProcessing = IntegrationsDatabase.Count;

            var integrationResult = new IntegrationResult()
            {
                Id = IntegrationId,
                CPU = "PC processor",
                RAM = 640,
                IsDistributed = false,
                ElapsedSeconds = 2.97,
                IsResultNotANumber = false,
                Result = 6.41
            };

            await _testingUnit.ProcessResultAsync(integrationResult);

            var reportsAfterProcessing = ReportsDatabase.Count;
            var integrationTasksAfterProcessing = IntegrationsDatabase.Count;

            Assert.NotEqual(reportsBeforeProcessing, reportsAfterProcessing);
            Assert.Equal(reportsAfterProcessing - 1, reportsBeforeProcessing);

            Assert.NotEqual(integrationTasksBeforeProcessing, integrationTasksAfterProcessing);
            Assert.Equal(integrationTasksAfterProcessing + 1, integrationTasksBeforeProcessing);
        }
    }
}
