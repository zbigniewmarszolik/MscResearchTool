using MScResearchTool.AccountCreator.Domain.Models;
using System.Threading.Tasks;

namespace MScResearchTool.AccountCreator.Domain.Services
{
    public interface IUsersService
    {
        Task PostUserAsync(User userToPost);
    }
}
