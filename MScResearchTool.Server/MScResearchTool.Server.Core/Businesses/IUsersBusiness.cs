using MScResearchTool.Server.Core.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IUsersBusiness
    {
        Task CreateNewUser(User user);
        Task<User> ReadUserByNameOnlyAsync(string username);
        Task<User> ReadUserByNameAndPasswordAsync(string username, string password);
        Task<bool> AreUsersInDatabase();
    }
}
