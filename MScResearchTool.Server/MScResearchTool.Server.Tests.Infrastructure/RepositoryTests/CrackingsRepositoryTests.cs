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
    public class CrackingsRepositoryTests
    {
        private ICrackingsRepository _crackingsRepository { get; set; }

        public CrackingsRepositoryTests()
        {
            _crackingsRepository = new CrackingsRepository();
        }

        [Fact]
        public void Create_StandardInput_CreatingRecord()
        {
            var cracking = new Cracking();

            var before = 0;
            var after = 0;

            using (var tx = new TransactionScope())
            {
                before = _crackingsRepository.Read().Count;
                _crackingsRepository.Create(cracking);
                after = _crackingsRepository.Read().Count;
            }

            Assert.NotEqual(before, after);
            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void Create_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _crackingsRepository.Create(null);
            });
        }

        [Fact]
        public void Read_Standard_ReadingLazy()
        {
            var cracking = new Cracking()
            {
                Distributions = new List<CrackingDistribution>
                {
                    new CrackingDistribution()
                }
            };

            IList<Cracking> results = null;

            using (var tx = new TransactionScope())
            {
                _crackingsRepository.Create(cracking);

                results = _crackingsRepository.Read();
            }

            Assert.NotNull(results);
        }

        [Fact]
        public void ReadEager_Standard_ReadingEager()
        {
            var cracking = new Cracking()
            {
                Distributions = new List<CrackingDistribution>
                {
                    new CrackingDistribution()
                }
            };

            foreach (var item in cracking.Distributions)
            {
                item.Task = cracking;
            }

            IList<Cracking> results = null;

            using (var tx = new TransactionScope())
            {
                _crackingsRepository.Create(cracking);

                results = _crackingsRepository.ReadEager();
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
            var cracking = new Cracking();

            var beforeDelete = 0;
            var afterDelete = 0;

            using (var tx = new TransactionScope())
            {
                _crackingsRepository.Create(cracking);
                beforeDelete = _crackingsRepository.Read().Count;

                _crackingsRepository.Delete(cracking.Id);
                afterDelete = _crackingsRepository.Read().Count;
            }

            Assert.NotEqual(beforeDelete, afterDelete);
            Assert.Equal(beforeDelete - 1, afterDelete);
        }

        [Fact]
        public void Delete_NotExistingId_ThrowingException()
        {
            var results = _crackingsRepository.Read();

            var rand = new Random();

            var identity = rand.Next();

            while (results.Any(x => x.Id == identity))
                identity = rand.Next();

            Assert.Throws<ArgumentNullException>(() =>
            {
                _crackingsRepository.Delete(identity);
            });
        }

        [Fact]
        public void Update_Standard_UpdatingRecord()
        {
            var initialPassword = "passOld";
            var updatedPassword = "passNew";

            var cracking = new Cracking()
            {
                ArchivePassword = initialPassword
            };

            _crackingsRepository.Create(cracking);

            var update = new Cracking()
            {
                Id = cracking.Id,
                ArchivePassword = updatedPassword
            };

            _crackingsRepository.Update(update);

            var result = _crackingsRepository.Read().First(x => x.Id == update.Id);

            Assert.Equal(updatedPassword, result.ArchivePassword);

            _crackingsRepository.Delete(cracking.Id);
        }

        [Fact]
        public void Update_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _crackingsRepository.Update(null);
            });
        }
    }
}
