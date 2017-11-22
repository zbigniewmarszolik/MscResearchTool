using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.BusinessTests;
using System;
using System.Linq;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class IntegrationDistributionsBusinessTests : IntegrationDistributionsBusinessTestsBase
    {
        private readonly IntegrationDistributionsBusiness _testingUnit;
        private int x = 5;

        public IntegrationDistributionsBusinessTests() : base()
        {
            _testingUnit = GetUniversalMockedUnit();
        }

        [Fact]
        public async void ReadAllEagerAsync_Standard_ReturningEager()
        {
            var result = await _testingUnit.ReadAllEagerAsync();

            Assert.NotNull(result);
            Assert.Equal(DistributionsDatabase.Count, result.Count);

            foreach(var item in result)
            {
                Assert.NotNull(item.Task);
            }
        }

        [Fact]
        public async void ReadAvailableAsync_Standard_ReturningAvailableEager()
        {
            var result = await _testingUnit.ReadAvailableAsync();

            Assert.NotNull(result);

            foreach (var item in result)
            {
                Assert.NotNull(item.Task);
            }

            foreach(var item in result)
            {
                Assert.True(item.IsAvailable);
                Assert.False(item.IsFinished);
            }
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(20, false)]
        public async void ReadByIdAsync_IdInput_ReturningLazyIfIdExists(int index, bool condition)
        {
            IntegrationDistribution result = null;

            try
            {
                result = await _testingUnit.ReadByIdAsync(index);
            }
            catch(Exception e)
            {
                result = null;
            }

            var isNull = true;

            if (result != null)
                isNull = false;

            Assert.NotEqual(condition, isNull);

            if (!isNull)
                Assert.Null(result.Task);
        }

        [Fact]
        public async void UpdateAsync_ItemInput_UpdatingSuccesfully()
        {
            var list = await _testingUnit.ReadAllEagerAsync();
            var update = list.First();

            var beforeUpdate = update.DeviceTime;
            var idHold = update.Id;

            var updatingValue = 99.0;

            update.DeviceTime = updatingValue;

            await _testingUnit.UpdateAsync(update);

            list = await _testingUnit.ReadAllEagerAsync();
            var updated = list.First(x => x.Id == idHold);

            var afterUpdate = updated.DeviceTime;

            Assert.Equal(updatingValue, afterUpdate);
            Assert.NotEqual(beforeUpdate, afterUpdate);
        }

        [Fact]
        public async void UnstuckTakenAsync_Standard_UnstuckingAll()
        {
            var list = await _testingUnit.ReadAllEagerAsync();

            var toTest = list.FirstOrDefault(x => x.IsAvailable == false && x.IsFinished == false);

            if (toTest == null)
                return;

            await _testingUnit.UnstuckTakenAsync();

            list = await _testingUnit.ReadAllEagerAsync();

            foreach(var item in list)
            {
                if (!item.IsFinished)
                    Assert.True(item.IsAvailable);
            }
        }

        [Fact]
        public async void UnstuckByIdAsync_IdInput_UnstuckingCertainObject()
        {
            var list = await _testingUnit.ReadAllEagerAsync();

            var toTest = list.FirstOrDefault(x => x.IsAvailable == false && x.IsFinished == false);

            if (toTest == null)
                return;

            var idHold = toTest.Id;

            await _testingUnit.UnstuckByIdAsync(toTest.Task.Id);

            var afterUnstuck = await _testingUnit.ReadByIdAsync(idHold);

            if (!afterUnstuck.IsFinished)
                Assert.True(afterUnstuck.IsAvailable);
        }
    }
}
