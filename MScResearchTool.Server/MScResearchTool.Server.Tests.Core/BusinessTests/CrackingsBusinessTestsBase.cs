using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.BusinessLogic.Factories;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class CrackingsBusinessTestsBase : MockBase<TestingUnit<CrackingsBusiness>>
    {
        protected int CrackingId { get; private set; }
        protected int DistributionFirstId { get; private set; }
        protected int DistributionSecondId { get; private set; }
        protected int NotExistingId { get; private set; }

        protected IList<Cracking> CrackingsDatabase { get; set; }

        public CrackingsBusinessTestsBase()
        {
            CrackingId = 1;
            DistributionFirstId = 31;
            DistributionSecondId = 32;

            CrackingsDatabase = CreateCrackings();

            var random = new Random();
            NotExistingId = random.Next(1, 1000);

            while (CrackingsDatabase.Any(x => x.Id == NotExistingId))
                NotExistingId = random.Next(1, 1000);
        }

        protected override TestingUnit<CrackingsBusiness> GetUnit()
        {
            var unit = new TestingUnit<CrackingsBusiness>();

            unit.AddDependency(new Mock<ICrackingsRepository>(MockBehavior.Strict));
            unit.AddDependency(new Mock<CrackingDistributionFactory>(MockBehavior.Strict));

            return unit;
        }

        protected CrackingsBusiness GetUniversalMockedUnit()
        {
            TestingUnit<CrackingsBusiness> testingUnit = GetUnit();

            var mockCrackingsRepository = testingUnit.GetDependency<Mock<ICrackingsRepository>>();

            mockCrackingsRepository.Setup(r => r.Read()).Returns(() =>
            {
                var ret = CrackingsDatabase;

                foreach (var item in ret)
                {
                    item.Distributions = null;
                }

                return ret;
            });

            mockCrackingsRepository.Setup(re => re.ReadEager()).Returns(CrackingsDatabase);

            mockCrackingsRepository.Setup(c => c.Create(It.IsAny<Cracking>())).Callback((Cracking freshCracking) =>
            {
                CrackingsDatabase.Add(freshCracking);
            }).Verifiable();

            mockCrackingsRepository.Setup(u => u.Update(It.IsAny<Cracking>())).Callback((Cracking updatedCracking) =>
            {
                var oldItem = CrackingsDatabase.First(x => x.Id == updatedCracking.Id);
                var index = CrackingsDatabase.IndexOf(oldItem);
                CrackingsDatabase[index] = updatedCracking;
            });

            mockCrackingsRepository.Setup(d => d.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var item = CrackingsDatabase.First(x => x.Id == id);
                CrackingsDatabase.Remove(item);
            }).Verifiable();

            var mockDistributionsFactory = testingUnit.GetDependency<Mock<CrackingDistributionFactory>>();

            mockDistributionsFactory.Setup(g => g.GetInstance(It.IsAny<Cracking>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns((Cracking task, double downLimit, double upLimit) =>
                {
                    var mockInstance = new CrackingDistribution()
                    {
                        FileName = "file.zip",
                        ArchivePassword = "passw0rd",
                        CreationDate = new DateTime(2017, 10, 1),
                        DeviceCPU = "arm 900MHz",
                        DeviceRAM = 512,
                        DeviceResult = null,
                        DeviceTime = 2.37,
                        Id = 998,
                        IsAvailable = true,
                        IsFinished = false,
                        Task = task
                    };

                    return mockInstance;
                });


            return testingUnit.GetResolvedTestingUnit();
        }

        protected Cracking CreateCrackingToDistribute()
        {
            var cracking = new Cracking()
            {
                FileName = "file.zip",
                ArchivePassword = "passw0rd",
                CreationDate = new DateTime(2017, 9, 30),
                DesktopCPU = "X6 1000MHz",
                DesktopRAM = 1024,
                DroidRanges = 2,
                FullResult = "passw0rd",
                FullTime = 4.19,
                Id = 997,
                IsAvailable = true,
                IsFinished = false,
                PartialResult = "passw0rd",
                PartialTime = 5.37
            };

            cracking.Distributions = new List<CrackingDistribution>()
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
                    Id = 998,
                    IsAvailable = true,
                    IsFinished = false,
                    Task = cracking
                },

                new CrackingDistribution()
                {
                    FileName = "file.zip",
                    ArchivePassword = "passw0rd",
                    CreationDate = new DateTime(2017, 10, 1),
                    DeviceCPU = "arm2 933MHz",
                    DeviceRAM = 512,
                    DeviceResult = "passw0rd",
                    DeviceTime = 3.0,
                    Id = 999,
                    IsAvailable = true,
                    IsFinished = false,
                    Task = cracking
                }
            };

            return cracking;
        }

        private IList<Cracking> CreateCrackings()
        {
            IList<Cracking> crackingsList = new List<Cracking>();

            var cracking = CreateSingleCracking(CrackingId, DistributionFirstId, DistributionSecondId);

            var anotherCracking = CreateSingleCracking(35, 57, 58);

            crackingsList.Add(cracking);
            crackingsList.Add(anotherCracking);

            return crackingsList;
        }

        private Cracking CreateSingleCracking(int crackingId, int firstDistId, int secondDistId)
        {
            var cracking = new Cracking()
            {
                FileName = "file.zip",
                ArchivePassword = "passw0rd",
                CreationDate = new DateTime(2017, 9, 30),
                DesktopCPU = "X6 1000MHz",
                DesktopRAM = 1024,
                DroidRanges = 2,
                FullResult = "passw0rd",
                FullTime = 4.19,
                Id = crackingId,
                IsAvailable = false,
                IsFinished = false,
                PartialResult = "passw0rd",
                PartialTime = 5.37
            };

            cracking.Distributions = new List<CrackingDistribution>()
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
                    Id = firstDistId,
                    IsAvailable = false,
                    IsFinished = true,
                    Task = cracking
                },

                new CrackingDistribution()
                {
                    FileName = "file.zip",
                    ArchivePassword = "passw0rd",
                    CreationDate = new DateTime(2017, 10, 1),
                    DeviceCPU = "arm2 933MHz",
                    DeviceRAM = 512,
                    DeviceResult = "passw0rd",
                    DeviceTime = 3.0,
                    Id = secondDistId,
                    IsAvailable = false,
                    IsFinished = true,
                    Task = cracking
                }
            };

            return cracking;
        }
    }
}
