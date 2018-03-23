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
    public class CrackingDistributionsRepositoryTests
    {
        private ICrackingDistributionsRepository _integrationDistributionsRepository { get; set; }

        public CrackingDistributionsRepositoryTests()
        {
            _integrationDistributionsRepository = new CrackingDistributionsRepository();
        }

        [Fact]
        public void Create_StandardInput_CreatingRecord()
        {
            var crackingDistribution = new CrackingDistribution();

            var before = 0;
            var after = 0;

            using (var tx = new TransactionScope())
            {
                before = _integrationDistributionsRepository.Read().Count;
                _integrationDistributionsRepository.Create(crackingDistribution);
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
            var crackingDistribution = new CrackingDistribution()
            {
                Task = new Cracking()
            };

            IList<CrackingDistribution> results = null;

            using (var tx = new TransactionScope())
            {
                _integrationDistributionsRepository.Create(crackingDistribution);

                results = _integrationDistributionsRepository.Read();
            }

            Assert.NotNull(results);
        }

        [Fact]
        public void ReadEager_Standard_ReadingEager()
        {
            var crackingDistribution = new CrackingDistribution();

            IList<CrackingDistribution> results = null;

            using (var tx = new TransactionScope())
            {
                crackingDistribution = SolveTestingRelation(crackingDistribution);

                _integrationDistributionsRepository.Create(crackingDistribution);

                results = _integrationDistributionsRepository.ReadEager();
            }

            Assert.NotNull(results);

            foreach (var item in results)
            {
                Assert.NotNull(item.Task);
                Assert.IsType<Cracking>(item.Task);
            }
        }

        [Fact]
        public void Delete_Standard_DeletingRecord()
        {
            var crackingDistribution = new CrackingDistribution();

            var beforeDelete = 0;
            var afterDelete = 0;

            using (var tx = new TransactionScope())
            {
                _integrationDistributionsRepository.Create(crackingDistribution);
                beforeDelete = _integrationDistributionsRepository.Read().Count;

                _integrationDistributionsRepository.Delete(crackingDistribution.Id);
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
            var initialPassword = "oldSecret";
            var updatedPassword = "newSecret";

            var crackingDistribution = new CrackingDistribution()
            {
                ArchivePassword = initialPassword
            };

            _integrationDistributionsRepository.Create(crackingDistribution);

            var update = new CrackingDistribution()
            {
                Id = crackingDistribution.Id,
                ArchivePassword = updatedPassword
            };

            _integrationDistributionsRepository.Update(update);

            var result = _integrationDistributionsRepository.Read().First(x => x.Id == update.Id);

            Assert.Equal(updatedPassword, result.ArchivePassword);

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

        private CrackingDistribution SolveTestingRelation(CrackingDistribution distribution)
        {
            var relationRepository = new CrackingsRepository();

            var cracking = new Cracking();

            relationRepository.Create(cracking);

            distribution.Task = cracking;

            return distribution;
        }
    }
}
