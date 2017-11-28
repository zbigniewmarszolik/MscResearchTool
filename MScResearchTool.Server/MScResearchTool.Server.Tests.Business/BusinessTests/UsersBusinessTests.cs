using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Tests.Core.BusinessTests;
using System;
using Xunit;

namespace MScResearchTool.Server.Tests.Business.BusinessTests
{
    public class UsersBusinessTests : UsersBusinessTestsBase
    {
        private readonly UsersBusiness _testingUnit;

        public UsersBusinessTests() : base()
        {
            _testingUnit = GetUniversalMockedUnit();
        }

        [Fact]
        public async void CreateNewUser_UserInput_Creating()
        {
            var before = UsersDatabase.Count;

            var user = new User
            {
                Name = "username",
                Password = "password"
            };

            await _testingUnit.CreateNewUserAsync(user);

            var after = UsersDatabase.Count;

            Assert.NotEqual(before, after);
            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void CreateNewUser_NullInput_ThrowingException()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _testingUnit.CreateNewUserAsync(null);
            });
        }

        [Fact]
        public async void ReadUserByNameOnlyAsync_ExistingName_ReturningObject()
        {
            var result = await _testingUnit.ReadUserByNameOnlyAsync(ExistingName);

            Assert.NotNull(result);
        }

        [Fact]
        public async void ReadUserByNameOnlyAsync_NotExistingName_ReturningNull()
        {
            var result = await _testingUnit.ReadUserByNameOnlyAsync("otherName");

            Assert.Null(result);
        }

        [Fact]
        public async void ReadUserByNameAndPasswordAsync_ExistingNameAndPassword_ReturningObject()
        {
            var result = await _testingUnit.ReadUserByNameAndPasswordAsync(ExistingName, ExistingPassword);

            Assert.NotNull(result);
        }

        [Fact]
        public async void ReadUserByNameAndPasswordAsync_NotExistingNameAndPassword_ReturningNull()
        {
            var result = await _testingUnit.ReadUserByNameAndPasswordAsync("otherName", "wrongPass");

            Assert.Null(result);
        }

        [Fact]
        public async void AreUsersInDatabase_IfUsersExist_ReturningTrue()
        {
            var result = await _testingUnit.AreUsersInDatabaseAsync();

            Assert.True(result);
        }

        [Fact]
        public async void AreUsersInDatabase_IfNoUsersExist_ReturningFalse()
        {
            UsersDatabase.Clear();

            var result = await _testingUnit.AreUsersInDatabaseAsync();

            Assert.False(result);
        }

        [Fact]
        public async void IsUsernameTakenAsync_IfTaken_ReturnsTrue()
        {
            var result = await _testingUnit.IsUsernameTakenAsync(ExistingName);

            Assert.True(result);
        }

        [Fact]
        public async void IsUsernameTakenAsync_IfFree_ReturnsFalse()
        {
            var result = await _testingUnit.IsUsernameTakenAsync("some not existing username");

            Assert.False(result);
        }
    }
}
