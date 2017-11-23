using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class IntegrationResultsBusinessTestsBase : MockingTestsBase<TestingUnit<IntegrationResultsBusiness>>
    {
        protected int IntegrationId { get; private set; }
        protected int DistributionFirstId { get; private set; }
        protected int DistributionSecondId { get; private set; }

        protected IList<Integration> IntegrationsDatabase { get; set; }
        protected IList<Report> ReportsDatabase { get; set; }

        public IntegrationResultsBusinessTestsBase()
        {
            IntegrationId = 15;
            DistributionFirstId = 51;
            DistributionSecondId = 57;

            IntegrationsDatabase = CreateIntegrations();

            ReportsDatabase = new List<Report>();
        }

        protected override TestingUnit<IntegrationResultsBusiness> GetUnit()
        {
            var unit = new TestingUnit<IntegrationResultsBusiness>();

            unit.AddDependency(new Mock<IIntegrationsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IIntegrationDistributionsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IReportsBusiness>(MockBehavior.Strict));

            return unit;
        }

        protected IntegrationResultsBusiness GetUniversalMockedUnit()
        {
            TestingUnit<IntegrationResultsBusiness> testingUnit = GetUnit();

            var mockDistributionsBusiness = testingUnit.GetDependency<Mock<IIntegrationDistributionsBusiness>>();

            mockDistributionsBusiness.Setup(rae => rae.ReadAllEagerAsync()).ReturnsAsync(() =>
            {
                IList<IntegrationDistribution> dists = new List<IntegrationDistribution>();

                foreach (var item in IntegrationsDatabase)
                {
                    foreach (var j in item.Distributions)
                    {
                        j.Task = item;
                        dists.Add(j);
                    }   
                }

                return dists;
            });

            mockDistributionsBusiness.Setup(u => u.UpdateAsync(It.IsAny<IntegrationDistribution>())).Callback((IntegrationDistribution updatedDistribution) =>
            {
                var integrationIndex = 0;
                var distributionIndex = 0;

                foreach (var item in IntegrationsDatabase)
                {
                    var oldItem = item.Distributions.FirstOrDefault(x => x.Id == updatedDistribution.Id);

                    if (oldItem != null)
                    {
                        integrationIndex = IntegrationsDatabase.IndexOf(item);
                        distributionIndex = item.Distributions.IndexOf(oldItem);
                    }
                }

                IntegrationsDatabase[integrationIndex].Distributions[distributionIndex] = updatedDistribution;
            });

            var mockIntegrationsBusiness = testingUnit.GetDependency<Mock<IIntegrationsBusiness>>();

            mockIntegrationsBusiness.Setup(rid => rid.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return IntegrationsDatabase.FirstOrDefault(x => x.Id == id);
            });

            mockIntegrationsBusiness.Setup(rae => rae.ReadAllEagerAsync()).ReturnsAsync(() =>
            {
                return IntegrationsDatabase;
            });

            mockIntegrationsBusiness.Setup(ua => ua.UpdateAsync(It.IsAny<Integration>())).Callback((Integration updatedIntegration) =>
            {
                var oldItem = IntegrationsDatabase.FirstOrDefault(x => x.Id == updatedIntegration.Id);

                if(oldItem != null)
                {
                    var index = IntegrationsDatabase.IndexOf(oldItem);
                    IntegrationsDatabase[index] = updatedIntegration;
                }
            }).Returns(Task.CompletedTask);

            mockIntegrationsBusiness.Setup(da => da.CascadeDeleteAsync(It.IsAny<int>())).Callback((int deletionId) =>
            {
                var toRemove = IntegrationsDatabase.FirstOrDefault(x => x.Id == deletionId);
                IntegrationsDatabase.Remove(toRemove);
            }).Returns(Task.CompletedTask);

            var mockReportsBusiness = testingUnit.GetDependency<Mock<IReportsBusiness>>();

            mockReportsBusiness.Setup(g => g.GenerateIntegrationReportAsync(It.IsAny<int>())).Callback((int id) =>
            {
                ReportsDatabase.Add(new Report());
            }).Returns(Task.CompletedTask);

            return testingUnit.GetResolvedTestingUnit();
        }

        private IList<Integration> CreateIntegrations()
        {
            IList<Integration> integrationsList = new List<Integration>();

            var integration = CreateSingleIntegration(IntegrationId, DistributionFirstId, DistributionSecondId);

            integrationsList.Add(integration);

            return integrationsList;
        }

        private Integration CreateSingleIntegration(int integrationId, int firstDistId, int secondDistId)
        {
            var integration = new Integration()
            {
                Accuracy = 10,
                CreationDate = new DateTime(2017, 9, 30),
                DownBoundary = 1,
                DroidIntervals = 2,
                Formula = "Sin(x-1)",
                Id = integrationId,
                IsAvailable = true,
                IsFinished = false,
                IsTrapezoidMethodRequested = true,
                UnresolvedFormula = "sin(x-1)",
                UpBoundary = 3,
                PartialTime = 2.5,
                PartialResult = 6.3,
                IsResultNaN = false
            };

            integration.Distributions = new List<IntegrationDistribution>()
            {
                new IntegrationDistribution()
                {
                    Accuracy = 3,
                    CreationDate = new DateTime(2017, 10, 1),
                    DownBoundary = 1,
                    Formula = "Sin(x-1)",
                    Id = firstDistId,
                    IsAvailable = false,
                    IsFinished = true,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = true,
                    UpBoundary = 2,
                    DeviceCPU = "mobileCPU1",
                    DeviceRAM = 256,
                    DeviceResult = 3.2,
                    DeviceTime = 1.3,
                    Task = integration
                },

                new IntegrationDistribution()
                {
                    Accuracy = 3,
                    CreationDate = new DateTime(2017, 10, 1),
                    DownBoundary = 2,
                    Formula = "Sin(x-1)",
                    Id = secondDistId,
                    IsAvailable = false,
                    IsFinished = true,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = true,
                    UpBoundary = 3,
                    DeviceCPU = "mobileCPU",
                    DeviceRAM = 256,
                    DeviceResult = 3.1,
                    DeviceTime = 1.2,
                    Task = integration
                }
            };

            return integration;
        }
    }
}
