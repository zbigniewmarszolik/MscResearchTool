using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class CrackingResultsBusinessTestsBase : MockBase<TestingUnit<CrackingResultsBusiness>>
    {
        protected int CrackingId { get; private set; }
        protected int DistributionFirstId { get; private set; }
        protected int DistributionSecondId { get; private set; }

        protected IList<Cracking> CrackingsDatabase { get; set; }
        protected IList<Report> ReportsDatabase { get; set; }

        public CrackingResultsBusinessTestsBase()
        {
            CrackingId = 15;
            DistributionFirstId = 51;
            DistributionSecondId = 57;

            CrackingsDatabase = CreateCrackings();

            ReportsDatabase = new List<Report>();
        }

        protected override TestingUnit<CrackingResultsBusiness> GetUnit()
        {
            var unit = new TestingUnit<CrackingResultsBusiness>();

            unit.AddDependency(new Mock<ICrackingsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<ICrackingDistributionsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IReportsBusiness>(MockBehavior.Strict));

            return unit;
        }

        protected CrackingResultsBusiness GetUniversalMockedUnit()
        {
            TestingUnit<CrackingResultsBusiness> testingUnit = GetUnit();

            var mockDistributionsBusiness = testingUnit.GetDependency<Mock<ICrackingDistributionsBusiness>>();

            mockDistributionsBusiness.Setup(rae => rae.ReadAllEagerAsync()).ReturnsAsync(() =>
            {
                IList<CrackingDistribution> dists = new List<CrackingDistribution>();

                foreach (var item in CrackingsDatabase)
                {
                    foreach (var j in item.Distributions)
                    {
                        j.Task = item;
                        dists.Add(j);
                    }
                }

                return dists;
            });

            mockDistributionsBusiness.Setup(u => u.UpdateAsync(It.IsAny<CrackingDistribution>())).Callback((CrackingDistribution updatedDistribution) =>
            {
                var crackingIndex = 0;
                var distributionIndex = 0;

                foreach (var item in CrackingsDatabase)
                {
                    var oldItem = item.Distributions.FirstOrDefault(x => x.Id == updatedDistribution.Id);

                    if (oldItem != null)
                    {
                        crackingIndex = CrackingsDatabase.IndexOf(item);
                        distributionIndex = item.Distributions.IndexOf(oldItem);
                    }
                }

                CrackingsDatabase[crackingIndex].Distributions[distributionIndex] = updatedDistribution;
            });

            var mockCrackingsBusiness = testingUnit.GetDependency<Mock<ICrackingsBusiness>>();

            mockCrackingsBusiness.Setup(rid => rid.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return CrackingsDatabase.FirstOrDefault(x => x.Id == id);
            });

            mockCrackingsBusiness.Setup(rae => rae.ReadAllEagerAsync()).ReturnsAsync(() =>
            {
                return CrackingsDatabase;
            });

            mockCrackingsBusiness.Setup(ua => ua.UpdateAsync(It.IsAny<Cracking>())).Callback((Cracking updatedCracking) =>
            {
                var oldItem = CrackingsDatabase.FirstOrDefault(x => x.Id == updatedCracking.Id);

                if (oldItem != null)
                {
                    var index = CrackingsDatabase.IndexOf(oldItem);
                    CrackingsDatabase[index] = updatedCracking;
                }
            }).Returns(Task.CompletedTask);

            mockCrackingsBusiness.Setup(da => da.CascadeDeleteAsync(It.IsAny<int>())).Callback((int deletionId) =>
            {
                var toRemove = CrackingsDatabase.FirstOrDefault(x => x.Id == deletionId);
                CrackingsDatabase.Remove(toRemove);
            }).Returns(Task.CompletedTask);

            var mockReportsBusiness = testingUnit.GetDependency<Mock<IReportsBusiness>>();

            mockReportsBusiness.Setup(g => g.GenerateCrackingReportAsync(It.IsAny<int>())).Callback((int id) =>
            {
                ReportsDatabase.Add(new Report());
            }).Returns(Task.CompletedTask);

            return testingUnit.GetResolvedTestingUnit();
        }

        private IList<Cracking> CreateCrackings()
        {
            IList<Cracking> crackingsList = new List<Cracking>();

            var cracking = CreateSingleCracking(CrackingId, DistributionFirstId, DistributionSecondId);

            crackingsList.Add(cracking);

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
