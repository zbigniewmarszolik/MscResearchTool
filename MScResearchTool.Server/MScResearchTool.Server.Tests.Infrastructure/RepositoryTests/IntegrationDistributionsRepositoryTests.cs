using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Xunit;

namespace MScResearchTool.Server.Tests.Infrastructure.RepositoryTests
{
    public class IntegrationDistributionsRepositoryTests
    {
        private IIntegrationDistributionsRepository _integrationDistributionsRepository { get; set; }

        public IntegrationDistributionsRepositoryTests()
        {
            _integrationDistributionsRepository = new IntegrationDistributionsRepository();
        }

        [Fact]
        public void Create_StandardInput_CreatingRecord()
        {
            var integrationDistribution = new IntegrationDistribution();

            var before = 0;
            var after = 0;

            using (var tx = new TransactionScope())
            {
                before = _integrationDistributionsRepository.Read().Count;
                _integrationDistributionsRepository.Create(integrationDistribution);
                after = _integrationDistributionsRepository.Read().Count;
            }

            Assert.NotEqual(before, after);
            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void Create_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _integrationDistributionsRepository.Create(null);
            });
        }

        [Fact]
        public void Read_Standard_ReadingLazy()
        {
            var integrationDistribution = new IntegrationDistribution()
            {
                Task = new Integration()
            };

            IList<IntegrationDistribution> results = null;

            using (var tx = new TransactionScope())
            {
                _integrationDistributionsRepository.Create(integrationDistribution);

                results = _integrationDistributionsRepository.Read();
            }

            Assert.NotNull(results);
        }

        [Fact]
        public void ReadEager_Standard_ReadingEager()
        {
            var integrationDistribution = new IntegrationDistribution();

            IList<IntegrationDistribution> results = null;

            using (var tx = new TransactionScope())
            {
                integrationDistribution = SolveTestingRelation(integrationDistribution);

                _integrationDistributionsRepository.Create(integrationDistribution);

                results = _integrationDistributionsRepository.ReadEager();
            }

            Assert.NotNull(results);

            foreach (var item in results)
            {
                Assert.NotNull(item.Task);
                Assert.IsType<Integration>(item.Task);
            }
        }

        [Fact]
        public void Delete_Standard_DeletingRecord()
        {
            var integrationDistribution = new IntegrationDistribution();

            var beforeDelete = 0;
            var afterDelete = 0;

            using (var tx = new TransactionScope())
            {
                _integrationDistributionsRepository.Create(integrationDistribution);
                beforeDelete = _integrationDistributionsRepository.Read().Count;

                _integrationDistributionsRepository.Delete(integrationDistribution.Id);
                afterDelete = _integrationDistributionsRepository.Read().Count;
            }

            Assert.NotEqual(beforeDelete, afterDelete);
            Assert.Equal(beforeDelete - 1, afterDelete);
        }

        [Fact]
        public void Delete_NotExistingId_ThrowingException()
        {
            var results = _integrationDistributionsRepository.Read();

            var rand = new Random();

            var identity = rand.Next();

            while (results.Any(x => x.Id == identity))
                identity = rand.Next();

            Assert.Throws<ArgumentNullException>(() =>
            {
                _integrationDistributionsRepository.Delete(identity);
            });
        }

        [Fact]
        public void Update_Standard_UpdatingRecord()
        {
            var initialAccuracy = 300;
            var updatedAccuracy = 100;

            var integrationDistribution = new IntegrationDistribution()
            {
                Accuracy = initialAccuracy
            };

            _integrationDistributionsRepository.Create(integrationDistribution);

            var update = new IntegrationDistribution()
            {
                Id = integrationDistribution.Id,
                Accuracy = updatedAccuracy
            };

            _integrationDistributionsRepository.Update(update);

            var result = _integrationDistributionsRepository.Read().First(x => x.Id == update.Id);

            Assert.Equal(updatedAccuracy, result.Accuracy);

            _integrationDistributionsRepository.Delete(update.Id);
        }

        [Fact]
        public void Update_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _integrationDistributionsRepository.Update(null);
            });
        }

        private IntegrationDistribution SolveTestingRelation(IntegrationDistribution distribution)
        {
            var relationRepository = new IntegrationsRepository();

            var integration = new Integration();

            relationRepository.Create(integration);

            distribution.Task = integration;

            return distribution;
        }
    }
}
