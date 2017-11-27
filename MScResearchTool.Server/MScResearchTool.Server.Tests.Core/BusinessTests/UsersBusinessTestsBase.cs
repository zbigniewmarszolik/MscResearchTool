using Moq;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Tests.Core.Core;
using MScResearchTool.Server.Tests.Core.Units;
using System.Collections.Generic;

namespace MScResearchTool.Server.Tests.Core.BusinessTests
{
    public abstract class UsersBusinessTestsBase : MockingTestsBase<TestingUnit<UsersBusiness>>
    {
        protected string ExistingName { get; private set; }
        protected string ExistingPassword { get; private set; }

        protected IList<User> UsersDatabase { get; set; }

        public UsersBusinessTestsBase()
        {
            ExistingName = "user";
            ExistingPassword = "pass";

            UsersDatabase = new List<User>()
            {
                new User
                {
                    Salt = "xcxcxzc",
                    Id = 241,
                    Name = ExistingName,
                    Password = ExistingPassword
                }
            };
        }

        protected override TestingUnit<UsersBusiness> GetUnit()
        {
            var unit = new TestingUnit<UsersBusiness>();

            unit.AddDependency(new Mock<IUsersRepository>(MockBehavior.Strict));

            return unit;
        }

        protected UsersBusiness GetUniversalMockedUnit()
        {
            TestingUnit<UsersBusiness> testingUnit = GetUnit();

            var mockUsersRepository = testingUnit.GetDependency<Mock<IUsersRepository>>();
            mockUsersRepository.Setup(cu => cu.CreateUser(It.IsAny<User>())).Callback((User user) => UsersDatabase.Add(user));
            mockUsersRepository.Setup(ru => ru.ReadUsers()).Returns(UsersDatabase);

            return testingUnit.GetResolvedTestingUnit();
        }
    }
}
