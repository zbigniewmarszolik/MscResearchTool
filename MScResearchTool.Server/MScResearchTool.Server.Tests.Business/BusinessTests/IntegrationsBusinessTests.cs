using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.BusinessTests;
using System;
using System.Linq;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class IntegrationsBusinessTests : IntegrationsBusinessTestsBase
    {
        private readonly IntegrationsBusiness _testingUnit;

        public IntegrationsBusinessTests() : base()
        {
            _testingUnit = GetUniversalMockedUnit();
        }

        [Fact]
        public async void DistributeAndPersistAsync_IntegrationInput_DistributingAndSaving()
        {
            var integrationToDistribute = CreateIntegrationToDistribute();

            var idHold = integrationToDistribute.Id;

            await _testingUnit.DistributeAndPersistAsync(integrationToDistribute);

            var getBack = await _testingUnit.ReadByIdAsync(idHold);

            Assert.NotNull(getBack);
        }

        [Fact]
        public async void DistributeAndPersistAsync_CertainInput_DistributingProperly()
        {
            var integrationToDistribute = CreateIntegrationToDistribute();

            var idHold = integrationToDistribute.Id;

            integrationToDistribute.UpBoundary = 4.0;
            integrationToDistribute.DownBoundary = 2.0;

            foreach(var item in integrationToDistribute.Distributions)
            {
                item.UpBoundary = 0.0;
                item.DownBoundary = 0.0;
            }

            await _testingUnit.DistributeAndPersistAsync(integrationToDistribute);

            var allEagerIntegrations = await _testingUnit.ReadAllEagerAsync();
            var getBack = allEagerIntegrations.First(x => x.Id == idHold);

            Assert.Equal(2.0, getBack.Distributions.First().DownBoundary);
            Assert.Equal(3.0, getBack.Distributions.First().UpBoundary);
            Assert.Equal(3.0, getBack.Distributions.Skip(1).Take(1).First().DownBoundary);
            Assert.Equal(4.0, getBack.Distributions.Skip(1).Take(1).First().UpBoundary);
        }

        [Fact]
        public void DistributeAndPersistAsync_NullInput_ThrowingException()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _testingUnit.DistributeAndPersistAsync(null);
            });
        }

        [Fact]
        public async void ReadAllEagerAsync_Standard_ReturningEager()
        {
            var result = await _testingUnit.ReadAllEagerAsync();

            Assert.NotNull(result);
            Assert.Equal(IntegrationsDatabase.Count, result.Count);

            foreach (var item in result)
            {
                Assert.NotNull(item.Distributions);
            }
        }

        [Fact]
        public async void ReadAvailableAsync_Standard_ReturningAvailableEager()
        {
            var result = await _testingUnit.ReadAvailableAsync();

            Assert.NotNull(result);

            foreach (var item in result)
            {
                Assert.NotNull(item.Distributions);
            }

            foreach (var item in result)
            {
                Assert.True(item.IsAvailable);
                Assert.False(item.IsFinished);
            }
        }

        [Fact]
        public async void ReadByIdAsync_ExistingIdInput_ReturningLazyIfIdExists()
        {
            var result = await _testingUnit.ReadByIdAsync(IntegrationId);

            Assert.NotNull(result);
            Assert.Null(result.Distributions);
        }

        [Fact]
        public async void ReadByIdAsync_WrongIdInput_ReturningLazyIfIdExists()
        {
            Integration result = null;

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                result = await _testingUnit.ReadByIdAsync(NotExistingId);
            });

            Assert.Null(result);
        }

        [Fact]
        public async void UpdateAsync_ItemInput_UpdatingSuccesfully()
        {
            var list = await _testingUnit.ReadAllEagerAsync();
            var update = list.First();

            var beforeUpdate = update.FullResult;
            var idHold = update.Id;

            var updatingResultValue = 114.392;

            update.FullResult = updatingResultValue;

            await _testingUnit.UpdateAsync(update);

            list = await _testingUnit.ReadAllEagerAsync();
            var updated = list.First(x => x.Id == idHold);

            var afterUpdate = updated.FullResult;

            Assert.Equal(updatingResultValue, afterUpdate);
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

            foreach (var item in list)
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

            await _testingUnit.UnstuckByIdAsync(idHold);

            var afterUnstuck = await _testingUnit.ReadByIdAsync(idHold);

            if (!afterUnstuck.IsFinished)
                Assert.True(afterUnstuck.IsAvailable);
        }

        [Fact]
        public async void CascadeDeleteAsync_ExistingId_DeletingWithCascade()
        {
            var list = await _testingUnit.ReadAllAsync();
            var beforeDelete = list.Count();

            await _testingUnit.CascadeDeleteAsync(IntegrationId);

            list = await _testingUnit.ReadAllAsync();
            var afterDelete = list.Count();

            Assert.NotEqual(beforeDelete, afterDelete);
            Assert.Equal(afterDelete + 1, beforeDelete);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
             {
                 var result = await _testingUnit.ReadByIdAsync(IntegrationId);
             });
        }

        [Fact]
        public async void CascadeDeleteAsync_WrongId_DeletingWithCascade()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _testingUnit.CascadeDeleteAsync(NotExistingId);
            });
        }
    }
}
