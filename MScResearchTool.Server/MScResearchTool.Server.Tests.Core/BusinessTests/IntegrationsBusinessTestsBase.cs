using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.BusinessLogic.Factories;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class IntegrationsBusinessTestsBase : MockBase<TestingUnit<IntegrationsBusiness>>
    {
        protected int IntegrationId { get; private set; }
        protected int DistributionFirstId { get; private set; }
        protected int DistributionSecondId { get; private set; }
        protected int NotExistingId { get; private set; }

        protected IList<Integration> IntegrationsDatabase { get; set; }

        public IntegrationsBusinessTestsBase()
        {
            IntegrationId = 1;
            DistributionFirstId = 31;
            DistributionSecondId = 32;

            IntegrationsDatabase = CreateIntegrations();

            var random = new Random();
            NotExistingId = random.Next(1, 1000);

            while (IntegrationsDatabase.Any(x => x.Id == NotExistingId))
                NotExistingId = random.Next(1, 1000);
        }

        protected override TestingUnit<IntegrationsBusiness> GetUnit()
        {
            var unit = new TestingUnit<IntegrationsBusiness>();

            unit.AddDependency(new Mock<IIntegrationsRepository>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IntegrationDistributionFactory>(MockBehavior.Strict));

            return unit;
        }

        protected IntegrationsBusiness GetUniversalMockedUnit()
        {
            TestingUnit<IntegrationsBusiness> testingUnit = GetUnit();

            var mockIntegrationsRepository = testingUnit.GetDependency<Mock<IIntegrationsRepository>>();

            mockIntegrationsRepository.Setup(r => r.Read()).Returns(() =>
            {
                var ret = IntegrationsDatabase;

                foreach (var item in ret)
                {
                    item.Distributions = null;
                }

                return ret;
            });

            mockIntegrationsRepository.Setup(re => re.ReadEager()).Returns(IntegrationsDatabase);

            mockIntegrationsRepository.Setup(c => c.Create(It.IsAny<Integration>())).Callback((Integration freshIntegration) =>
            {
                IntegrationsDatabase.Add(freshIntegration);
            }).Verifiable();

            mockIntegrationsRepository.Setup(u => u.Update(It.IsAny<Integration>())).Callback((Integration updatedIntegration) =>
            {
                var oldItem = IntegrationsDatabase.First(x => x.Id == updatedIntegration.Id);
                var index = IntegrationsDatabase.IndexOf(oldItem);
                IntegrationsDatabase[index] = updatedIntegration;
            });

            mockIntegrationsRepository.Setup(d => d.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var item = IntegrationsDatabase.First(x => x.Id == id);
                IntegrationsDatabase.Remove(item);
            }).Verifiable();

            var mockDistributionsFactory = testingUnit.GetDependency<Mock<IntegrationDistributionFactory>>();

            mockDistributionsFactory.Setup(g => g.GetInstance(It.IsAny<Integration>(), It.IsAny<double>(), It.IsAny<double>()))
                .Returns((Integration task, double downLimit, double upLimit) =>
                {
                    var mockInstance = new IntegrationDistribution()
                    {
                        Accuracy = task.Accuracy / task.DroidIntervals,
                        CreationDate = task.CreationDate,
                        DownBoundary = downLimit,
                        UpBoundary = upLimit,
                        Formula = task.Formula,
                        IsTrapezoidMethodRequested = task.IsTrapezoidMethodRequested,
                        Task = task,
                        IsAvailable = true,
                        IsFinished = false,
                        IsResultNaN = false,
                        DeviceRAM = 0,
                        DeviceCPU = "Unknown"
                    };

                    return mockInstance;
                });


            return testingUnit.GetResolvedTestingUnit();
        }

        protected Integration CreateIntegrationToDistribute()
        {
            var integration = new Integration()
            {
                Accuracy = 10,
                CreationDate = new DateTime(2017, 9, 30),
                DownBoundary = 1,
                DroidIntervals = 2,
                Formula = "Sin(x-1)",
                Id = 997,
                IsAvailable = true,
                IsFinished = false,
                IsTrapezoidMethodRequested = true,
                UnresolvedFormula = "sin(x-1)",
                UpBoundary = 3
            };

            integration.Distributions = new List<IntegrationDistribution>()
            {
                new IntegrationDistribution()
                {
                    Accuracy = 3,
                    CreationDate = new DateTime(2017, 10, 1),
                    DownBoundary = 1,
                    Formula = "Sin(x-1)",
                    Id = 998,
                    IsAvailable = true,
                    IsFinished = false,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = true,
                    UpBoundary = 2
                },

                new IntegrationDistribution()
                {
                    Accuracy = 3,
                    CreationDate = new DateTime(2017, 10, 1),
                    DownBoundary = 2,
                    Formula = "Sin(x-1)",
                    Id = 999,
                    IsAvailable = true,
                    IsFinished = false,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = true,
                    UpBoundary = 3
                }
            };

            return integration;
        }

        private IList<Integration> CreateIntegrations()
        {
            IList<Integration> integrationsList = new List<Integration>();

            var integration = CreateSingleIntegration(IntegrationId, DistributionFirstId, DistributionSecondId);

            var anotherIntegration = CreateSingleIntegration(35, 57, 58);

            integrationsList.Add(integration);
            integrationsList.Add(anotherIntegration);

            return integrationsList;
        }

        private Integration CreateSingleIntegration(int integrationId, int firstDistId, int secondDistId)
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
                Id = integrationId,
                IsAvailable = false,
                IsFinished = false,
                IsResultNaN = false,
                IsTrapezoidMethodRequested = false,
                PartialResult = 15.0,
                PartialTime = 5.37,
                UnresolvedFormula = "sin(x-1)",
                UpBoundary = 3
            };

            integration.Distributions = new List<IntegrationDistribution>()
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
                    Id = firstDistId,
                    IsAvailable = false,
                    IsFinished = true,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = false,
                    Task = null,
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
                    Id = secondDistId,
                    IsAvailable = false,
                    IsFinished = true,
                    IsResultNaN = false,
                    IsTrapezoidMethodRequested = false,
                    Task = null,
                    UpBoundary = 3
                }
            };

            return integration;
        }
    }
}
