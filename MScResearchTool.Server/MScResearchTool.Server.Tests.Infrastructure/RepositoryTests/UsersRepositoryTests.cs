using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Transactions;
using Xunit;

namespace MScResearchTool.Server.Tests.Infrastructure.RepositoryTests
{
    public class UsersRepositoryTests
    {
        private IUsersRepository _usersRepository { get; set; }

        public UsersRepositoryTests()
        {
            _usersRepository = new UsersRepository();
        }

        [Fact]
        public void CreateUser_StandardInput_CreatingUser()
        {
            var user = new User();

            var before = 0;
            var after = 0;

            using (var tx = new TransactionScope())
            {
                before = _usersRepository.ReadUsers().Count;
                _usersRepository.CreateUser(user);
                after = _usersRepository.ReadUsers().Count;
            }

            Assert.NotEqual(before, after);
            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void CreateUser_Null_ThrowingException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _usersRepository.CreateUser(null);
            });
        }

        [Fact]
        public void ReadUsers_Standard_Reading()
        {
            var user = new User();

            IList<User> users = null;

            using (var tx = new TransactionScope())
            {
                _usersRepository.CreateUser(user);

                users = _usersRepository.ReadUsers();
            }

            Assert.NotNull(users);
        }
    }
}
