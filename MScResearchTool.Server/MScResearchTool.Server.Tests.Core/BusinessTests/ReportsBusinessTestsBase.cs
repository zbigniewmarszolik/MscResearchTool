﻿using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class ReportsBusinessTestsBase : MockBase<TestingUnit<ReportsBusiness>>
    {
        protected int FirstId { get; private set; }
        protected int SecondId { get; private set; }
        protected int CrackingId { get; private set; }
        protected int IntegrationId { get; private set; }
        protected int DistributionFirstId { get; private set; }
        protected int DistributionSecondId { get; private set; }

        protected IList<Report> ReportsDatabase { get; set; }

        public ReportsBusinessTestsBase()
        {
            FirstId = 3;
            SecondId = 10;
            CrackingId = 30;
            IntegrationId = 5;
            DistributionFirstId = 10;
            DistributionSecondId = 11;

            ReportsDatabase = new List<Report>()
            {
                new Report(),

                new Report()
                {
                    Id = SecondId,
                    GenerationDate = new DateTime(2017, 1, 15)
                },

                new Report()
                {
                    Id = FirstId,
                    GenerationDate = new DateTime(2017, 3, 24)
                }
            };
        }

        protected override TestingUnit<ReportsBusiness> GetUnit()
        {
            var unit = new TestingUnit<ReportsBusiness>();

            unit.AddDependency(new Mock<ICrackingsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IIntegrationsBusiness>(MockBehavior.Strict));
            unit.AddDependency(new Mock<IReportsRepository>(MockBehavior.Strict));

            return unit;
        }

        protected ReportsBusiness GetUniversalMockedUnit()
        {
            TestingUnit<ReportsBusiness> testingUnit = GetUnit();

            var mockReportsRepository = testingUnit.GetDependency<Mock<IReportsRepository>>();

            mockReportsRepository.Setup(r => r.Read()).Returns(ReportsDatabase);

            mockReportsRepository.Setup(d => d.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var item = ReportsDatabase.First(x => x.Id == id);
                ReportsDatabase.Remove(item);
            }).Verifiable();

            mockReportsRepository.Setup(c => c.Create(It.IsAny<Report>())).Callback((Report freshReport) =>
            {
                ReportsDatabase.Add(freshReport);
            }).Verifiable();

            var mockIntegrationsBusiness = testingUnit.GetDependency<Mock<IIntegrationsBusiness>>();

            mockIntegrationsBusiness.Setup(re => re.ReadAllEagerAsync()).ReturnsAsync(new List<Integration>()
            {
                CreateIntegration()
            });

            var mockCrackingsBusiness = testingUnit.GetDependency<Mock<ICrackingsBusiness>>();

            mockCrackingsBusiness.Setup(re => re.ReadAllEagerAsync()).ReturnsAsync(new List<Cracking>()
            {
                CreateCracking()
            });

            return testingUnit.GetResolvedTestingUnit();
        }

        protected Integration CreateIntegration()
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
                Id = IntegrationId,
                IsAvailable = false,
                IsFinished = true,
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
                    Id = DistributionFirstId,
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
                    Id = DistributionSecondId,
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

        protected Cracking CreateCracking()
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
                Id = CrackingId,
                IsAvailable = false,
                IsFinished = true,
                PartialResult = "passw0rd",
                PartialTime = 5.37,
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
                    Id = DistributionFirstId,
                    IsAvailable = false,
                    IsFinished = true,
                    Task = null,
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
                    Id = DistributionSecondId,
                    IsAvailable = false,
                    IsFinished = true,
                    Task = null,
                }
            };

            return cracking;
        }
    }
}
