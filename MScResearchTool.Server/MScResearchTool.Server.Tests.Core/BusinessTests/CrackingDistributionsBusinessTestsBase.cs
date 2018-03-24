using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class CrackingDistributionsBusinessTestsBase : MockBase<TestingUnit<CrackingDistributionsBusiness>>
    {
        protected int CrackingId { get; private set; }
        protected int DistributionFirstId { get; private set; }
        protected int DistributionSecondId { get; private set; }
        protected int NotExistingId { get; private set; }

        protected IList<CrackingDistribution> DistributionsDatabase { get; set; }

        public CrackingDistributionsBusinessTestsBase()
        {
            CrackingId = 42;
            DistributionFirstId = 10;
            DistributionSecondId = 11;

            var random = new Random();
            NotExistingId = random.Next(1, 1000);

            DistributionsDatabase = CreateDistributions();

            while (NotExistingId == DistributionFirstId && NotExistingId == DistributionSecondId)
                NotExistingId = random.Next(1, 1000);
        }

        protected override TestingUnit<CrackingDistributionsBusiness> GetUnit()
        {
            var unit = new TestingUnit<CrackingDistributionsBusiness>();

            unit.AddDependency(new Mock<ICrackingDistributionsRepository>(MockBehavior.Strict));

            return unit;
        }

        protected CrackingDistributionsBusiness GetUniversalMockedUnit()
        {
            TestingUnit<CrackingDistributionsBusiness> testingUnit = GetUnit();

            var mockDistributionsRepository = testingUnit.GetDependency<Mock<ICrackingDistributionsRepository>>();

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

            mockDistributionsRepository.Setup(c => c.Create(It.IsAny<CrackingDistribution>())).Callback((CrackingDistribution freshDistribution) =>
            {
                DistributionsDatabase.Add(freshDistribution);
            }).Verifiable();

            mockDistributionsRepository.Setup(u => u.Update(It.IsAny<CrackingDistribution>())).Callback((CrackingDistribution updatedDistribution) =>
            {
                var oldItem = DistributionsDatabase.First(x => x.Id == updatedDistribution.Id);
                var index = DistributionsDatabase.IndexOf(oldItem);
                DistributionsDatabase[index] = updatedDistribution;
            });

            mockDistributionsRepository.Setup(d => d.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var item = DistributionsDatabase.First(x => x.Id == id);
                DistributionsDatabase.Remove(item);
            }).Verifiable();

            return testingUnit.GetResolvedTestingUnit();
        }

        private IList<CrackingDistribution> CreateDistributions()
        {
            var task = CreateTask();

            var list = new List<CrackingDistribution>()
            {
                new CrackingDistribution()
                {
                    FileName = "file.zip",
                    ArchivePassword = "passw0rd",
                    CreationDate = new DateTime(2017, 10, 1),
                    DeviceCPU = "arm 900MHz",
                    DeviceRAM = 512,
                    DeviceResult = null,
                    DeviceTime = 2.37,
                    Id = 10,
                    IsAvailable = false,
                    IsFinished = true,
                    Task = task
                },

                new CrackingDistribution()
                {
                    FileName = "file.zip",
                    ArchivePassword = "passw0rd",
                    CreationDate = new DateTime(2017, 10, 1),
                    DeviceCPU = "arm 900MHz",
                    DeviceRAM = 512,
                    DeviceResult = null,
                    DeviceTime = 2.37,
                    Id = 11,
                    IsAvailable = false,
                    IsFinished = false,
                    Task = task
                }
            };

            return list;
        }

        private Cracking CreateTask()
        {
            var integration = new Cracking()
            {
                FileName = "file.zip",
                ArchivePassword = "passw0rd",
                CreationDate = new DateTime(2017, 9, 30),
                DesktopCPU = "X6 1000MHz",
                DesktopRAM = 1024,
                DroidRanges = 2,
                FullResult = "passw0rd",
                FullTime = 4.19,
                Id = CrackingId,
                IsAvailable = false,
                IsFinished = true,
                PartialResult = "passw0rd",
                PartialTime = 5.37
            };

            return integration;
        }
    }
}
