using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.BusinessTests;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class ReportsBusinessTests : ReportsBusinessTestsBase
    {
        private readonly ReportsBusiness _testingUnit;

        public ReportsBusinessTests() : base()
        {
            _testingUnit = GetUniversalMockedUnit();
        }

        [Fact]
        public async void ReadAllAsync_Standard_ReturningReportsAsync()
        {
            var result = await _testingUnit.ReadAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<Report>>(result);
        }

        [Fact]
        public async void ReadByIdAsync_IdInput_ReturningSingleReport()
        {
            var result = await _testingUnit.ReadByIdAsync(FirstId);
            var resultTwo = await _testingUnit.ReadByIdAsync(SecondId);

            Assert.NotNull(result);
            Assert.NotNull(resultTwo);

            Assert.NotEqual(result.GenerationDate, resultTwo.GenerationDate);
            Assert.Equal(FirstId, result.Id);
            Assert.Equal(SecondId, resultTwo.Id);
        }

        [Fact]
        public async void DeleteAsync_IdInput_DeletingFromRepository()
        {
            await _testingUnit.DeleteAsync(FirstId);

            var result = await _testingUnit.ReadAllAsync();

            var verify = result.FirstOrDefault(x => x.Id == FirstId);

            Assert.Null(verify);
        }

        [Fact]
        public async void GenerateIntegrationReportAsync_IdInput_CreatingNewReport()
        {
            var res = await _testingUnit.ReadAllAsync();
            var before = res.Count;

            await _testingUnit.GenerateIntegrationReportAsync(IntegrationId);

            var result = await _testingUnit.ReadAllAsync();

            var after = result.Count;

            Assert.NotNull(result);

            Assert.Equal(before + 1, after);
        }
    }
}
