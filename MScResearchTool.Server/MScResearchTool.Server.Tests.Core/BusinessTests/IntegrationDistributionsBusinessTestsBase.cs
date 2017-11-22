using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class IntegrationDistributionsBusinessTestsBase : MockingTestsBase<TestingUnit<IntegrationDistributionsBusiness>>
    {
        protected IList<IntegrationDistribution> DistributionsDatabase { get; set; }

        public IntegrationDistributionsBusinessTestsBase()
        {
            DistributionsDatabase = CreateDistributions();
        }

        protected override TestingUnit<IntegrationDistributionsBusiness> GetUnit()
        {
            var unit = new TestingUnit<IntegrationDistributionsBusiness>();

            unit.AddDependency(new Mock<IIntegrationDistributionsRepository>(MockBehavior.Strict));

            return unit;
        }

        protected IntegrationDistributionsBusiness GetUniversalMockedUnit()
        {
            TestingUnit<IntegrationDistributionsBusiness> testingUnit = GetUnit();

            var mockDistributionsRepository = testingUnit.GetDependency<Mock<IIntegrationDistributionsRepository>>();

            mockDistributionsRepository.Setup(r => r.Read()).Returns(() =>
            {
                var ret = DistributionsDatabase;

                foreach (var item in ret)
                {
                    item.Task = null;
                }

                return ret;
            });

            mockDistributionsRepository.Setup(re => re.ReadEager()).Returns(DistributionsDatabase);

            mockDistributionsRepository.Setup(c => c.Create(It.IsAny<IntegrationDistribution>())).Callback((IntegrationDistribution freshDistribution) =>
            {
                DistributionsDatabase.Add(freshDistribution);
            }).Verifiable();

            mockDistributionsRepository.Setup(u => u.Update(It.IsAny<IntegrationDistribution>())).Callback((IntegrationDistribution updatedDistribution) =>
            {
                var oldItem = DistributionsDatabase.First(x => x.Id == updatedDistribution.Id);
                var index = DistributionsDatabase.IndexOf(oldItem);
                DistributionsDatabase[index] = updatedDistribution;

                var item = DistributionsDatabase.First(x => x.Id == updatedDistribution.Id);
                DistributionsDatabase.Remove(item);
                DistributionsDatabase.Add(updatedDistribution);
            });

            mockDistributionsRepository.Setup(d => d.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var item = DistributionsDatabase.First(x => x.Id == id);
                DistributionsDatabase.Remove(item);
            }).Verifiable();

            return testingUnit.GetResolvedTestingUnit();
        }

        private IList<IntegrationDistribution> CreateDistributions()
        {
            var task = CreateTask();

            var list = new List<IntegrationDistribution>()
            {
                new IntegrationDistribution()
                {
                    Accuracy = 3,
                    CreationDate = new DateTime(2017, 10, 1),
                    DeviceCPU = "arm 900MHz",
                    DeviceRAM = 512,
                    DeviceResult = 14.99,
                    DeviceTime = 2.37,
                    DownBoundary = 1,
                    Formula = "Sin(x-1)",
                    Id = 10,
                    IsAvailable = false,
                    IsFinished = true,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = false,
                    Task = task,
                    UpBoundary = 2
                },

                new IntegrationDistribution()
                {
                    Accuracy = 3,
                    CreationDate = new DateTime(2017, 10, 1),
                    DeviceCPU = "arm2 933MHz",
                    DeviceRAM = 512,
                    DeviceResult = 0.01,
                    DeviceTime = 3.0,
                    DownBoundary = 2,
                    Formula = "Sin(x-1)",
                    Id = 11,
                    IsAvailable = false,
                    IsFinished = false,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = false,
                    Task = task,
                    UpBoundary = 3
                }
            };

            return list;
        }

        private Integration CreateTask()
        {
            var integration = new Integration()
            {
                Accuracy = 10,
                CreationDate = new DateTime(2017, 9, 30),
                DesktopCPU = "X6 1000MHz",
                DesktopRAM = 1024,
                DownBoundary = 1,
                DroidIntervals = 2,
                Formula = "Sin(x-1)",
                FullResult = 15.0,
                FullTime = 4.19,
                Id = 42,
                IsAvailable = false,
                IsFinished = true,
                IsResultNaN = false,
                IsTrapezoidMethodRequested = false,
                PartialResult = 15.0,
                PartialTime = 5.37,
                UnresolvedFormula = "sin(x-1)",
                UpBoundary = 3
            };

            return integration;
        }
    }
}