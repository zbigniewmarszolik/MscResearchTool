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
    public class ReportsRepositoryTests
    {
        private IReportsRepository _reportsRepository { get; set; }

        public ReportsRepositoryTests()
        {
            _reportsRepository = new ReportsRepository();
        }

        [Fact]
        public void Create_StandardInput_CreatingRecord()
        {
            var report = new Report();

            var before = 0;
            var after = 0;

            using (var tx = new TransactionScope())
            {
                before = _reportsRepository.Read().Count;
                _reportsRepository.Create(report);
                after = _reportsRepository.Read().Count;
            }

            Assert.NotEqual(before, after);
            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void Create_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _reportsRepository.Create(null);
            });
        }

        [Fact]
        public void Read_Standard_Reading()
        {
            var report = new Report();

            IList<Report> results = null;

            using (var tx = new TransactionScope())
            {
                _reportsRepository.Create(report);

                results = _reportsRepository.Read();
            }

            Assert.NotNull(results);
        }

        [Fact]
        public void Delete_Standard_DeletingRecord()
        {
            var report = new Report();

            var beforeDelete = 0;
            var afterDelete = 0;

            using (var tx = new TransactionScope())
            {
                _reportsRepository.Create(report);
                beforeDelete = _reportsRepository.Read().Count;

                _reportsRepository.Delete(report.Id);
                afterDelete = _reportsRepository.Read().Count;
            }

            Assert.NotEqual(beforeDelete, afterDelete);
            Assert.Equal(beforeDelete - 1, afterDelete);
        }

        [Fact]
        public void Delete_NotExistingId_ThrowingException()
        {
            var results = _reportsRepository.Read();

            var rand = new Random();

            var identity = rand.Next();

            while (results.Any(x => x.Id == identity))
                identity = rand.Next();

            Assert.Throws<ArgumentNullException>(() =>
            {
                _reportsRepository.Delete(identity);
            });
        }
    }
}
