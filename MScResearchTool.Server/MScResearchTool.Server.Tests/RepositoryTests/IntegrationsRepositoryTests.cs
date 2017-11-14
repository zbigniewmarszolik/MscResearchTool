using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Xunit;

namespace MScResearchTool.Server.Tests.RepositoryTests
{
    public class IntegrationsRepositoryTests
    {
        private IIntegrationsRepository _integrationsRepository { get; set; }

        public IntegrationsRepositoryTests()
        {
            _integrationsRepository = new IntegrationsRepository();
        }

        [Fact]
        public void Create_StandardInput_CreatingRecord()
        {
            var integration = new Integration();

            var before = 0;
            var after = 0;

            using (var tx = new TransactionScope())
            {
                before = _integrationsRepository.Read().Count;
                _integrationsRepository.Create(integration);
                after = _integrationsRepository.Read().Count;
            }

            Assert.NotEqual(before, after);
            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void Create_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _integrationsRepository.Create(null);
            });
        }

        [Fact]
        public void Read_Standard_ReadingLazy()
        {
            var integration = new Integration()
            {
                Distributions = new List<IntegrationDistribution>
                {
                    new IntegrationDistribution()
                }
            };

            IList<Integration> results = null;

            using (var tx = new TransactionScope())
            {
                _integrationsRepository.Create(integration);

                results = _integrationsRepository.Read();
            }

            Assert.NotNull(results);
        }

        [Fact]
        public void Read_Standard_ReadingEager()
        {
            var integration = new Integration()
            {
                Distributions = new List<IntegrationDistribution>
                {
                    new IntegrationDistribution()
                }
            };

            foreach (var item in integration.Distributions)
            {
                item.Task = integration;
            }

            IList<Integration> results = null;

            using (var tx = new TransactionScope())
            {
                _integrationsRepository.Create(integration);

                results = _integrationsRepository.ReadEager();
            }

            Assert.NotNull(results);

            foreach (var item in results)
            {
                Assert.NotNull(item.Distributions);
                Assert.NotEqual(0, item.Distributions.Count);
            }
        }

        [Fact]
        public void Delete_Standard_DeletingRecord()
        {
            var integration = new Integration();

            var beforeDelete = 0;
            var afterDelete = 0;

            using (var tx = new TransactionScope())
            {
                _integrationsRepository.Create(integration);
                beforeDelete = _integrationsRepository.Read().Count;

                _integrationsRepository.Delete(integration.Id);
                afterDelete = _integrationsRepository.Read().Count;
            }

            Assert.NotEqual(beforeDelete, afterDelete);
            Assert.Equal(beforeDelete - 1, afterDelete);
        }

        [Fact]
        public void Delete_NotExistingId_ThrowingException()
        {
            var results = _integrationsRepository.Read();

            var rand = new Random();

            var identity = rand.Next();

            while (results.Any(x => x.Id == identity))
                identity = rand.Next();

            Assert.Throws<ArgumentNullException>(() =>
            {
                _integrationsRepository.Delete(identity);
            });
        }

        [Fact]
        public void Update_Standard_UpdatingRecord()
        {
            var initialAccuracy = 500;
            var updatedAccuracy = 750;

            var integration = new Integration()
            {
                Accuracy = initialAccuracy
            };

            _integrationsRepository.Create(integration);

            var update = new Integration()
            {
                Id = integration.Id,
                Accuracy = updatedAccuracy
            };

            _integrationsRepository.Update(update);

            var result = _integrationsRepository.Read().First(x => x.Id == update.Id);

            Assert.Equal(updatedAccuracy, result.Accuracy);

            _integrationsRepository.Delete(update.Id);
        }

        [Fact]
        public void Update_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _integrationsRepository.Update(null);
            });
        }
    }
}
