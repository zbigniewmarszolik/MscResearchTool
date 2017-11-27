using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Repositories
{
    public interface IUsersRepository
    {
        void CreateUser(User user);
        IList<User> ReadUsers();
    }
}
