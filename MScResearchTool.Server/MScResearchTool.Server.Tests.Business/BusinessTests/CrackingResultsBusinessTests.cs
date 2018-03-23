using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.BusinessTests;
using System;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class CrackingResultsBusinessTests : CrackingResultsBusinessTestsBase
    {
        private readonly CrackingResultsBusiness _testingUnit;

        public CrackingResultsBusinessTests()
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
        public async void ProcessResultAsync_ProperInput_GenerateReport()
        {
            var reportsBeforeProcessing = ReportsDatabase.Count;
            var crackingTasksBeforeProcessing = CrackingsDatabase.Count;

            var crackingResult = new CrackingResult()
            {
                Id = CrackingId,
                CPU = "PC processor",
                RAM = 640,
                IsDistributed = false,
                ElapsedSeconds = 2.97,
                PasswordResult = "passw0rd"
            };

            await _testingUnit.ProcessResultAsync(crackingResult);

            var reportsAfterProcessing = ReportsDatabase.Count;
            var crackingTasksAfterProcessing = CrackingsDatabase.Count;

            Assert.NotEqual(reportsBeforeProcessing, reportsAfterProcessing);
            Assert.Equal(reportsAfterProcessing - 1, reportsBeforeProcessing);

            Assert.NotEqual(crackingTasksBeforeProcessing, crackingTasksAfterProcessing);
            Assert.Equal(crackingTasksAfterProcessing + 1, crackingTasksBeforeProcessing);
        }

        [Fact]
        public async void ProcessResultAsync_WrongPassword_ThrowsArgumentException()
        {
            var crackingResult = new CrackingResult()
            {
                Id = CrackingId,
                CPU = "PC processor",
                RAM = 640,
                IsDistributed = false,
                ElapsedSeconds = 2.97,
                PasswordResult = "secret15"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 await _testingUnit.ProcessResultAsync(crackingResult);
             });
        }
    }
}
