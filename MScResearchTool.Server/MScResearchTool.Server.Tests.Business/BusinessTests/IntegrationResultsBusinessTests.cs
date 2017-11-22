using MScResearchTool.Server.Tests.Core.BusinessTests;
using System;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class IntegrationResultsBusinessTests : IntegrationResultsBusinessTestsBase
    {
        [Fact]
        public async void ProcessResultAsync_NullInput_ThrowingException()
        {
            var testingUnit = GetUnit().GetResolvedTestingUnit();

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
             {
                 await testingUnit.ProcessResultAsync(null);
             });
        }
    }
}
