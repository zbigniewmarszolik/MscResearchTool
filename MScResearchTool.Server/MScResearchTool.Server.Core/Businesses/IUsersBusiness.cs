using MScResearchTool.Server.Core.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface IUsersBusiness
    {
        Task CreateNewUserAsync(User user);
        Task<User> ReadUserByNameOnlyAsync(string username);
        Task<User> ReadUserByNameAndPasswordAsync(string username, string password);
        Task<bool> AreUsersInDatabaseAsync();
        Task<bool> IsUsernameTakenAsync(string username);
    }
}
