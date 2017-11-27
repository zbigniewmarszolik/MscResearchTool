using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class UsersBusiness : IUsersBusiness
    {
        private IUsersRepository _usersRepository { get; set; }

        public UsersBusiness(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task CreateNewUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            await Task.Run(() => _usersRepository.CreateUser(user));
        }

        public async Task<User> ReadUserByNameOnlyAsync(string username)
        {
            IList<User> users = null;
            User user = null;

            users = await ReadAllUsers();

            if (users != null)
                await Task.Run(() =>
                {
                    user = users.FirstOrDefault(x => x.Name == username);
                });

            return user;
        }

        public async Task<User> ReadUserByNameAndPasswordAsync(string username, string password)
        {
            IList<User> users = null;
            User user = null;

            users = await ReadAllUsers();

            if (users != null)
                await Task.Run(() =>
                {
                    user = users.FirstOrDefault(x => x.Name == username && x.Password == password);
                });

            return user;
        }

        public async Task<bool> AreUsersInDatabaseAsync()
        {
            IList<User> users = null;

            users = await ReadAllUsers();

            if (users != null)
            {
                if (users.Count > 0)
                    return true;

                else return false;
            }

            else return false;
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            IList<User> users = null;

            users = await ReadAllUsers();

            var isTaken = users.Any(x => x.Name == username);

            return isTaken;
        }

        private async Task<IList<User>> ReadAllUsers()
        {
            IList<User> allUsers = null;

            await Task.Run(() =>
            {
                allUsers = _usersRepository.ReadUsers();
            });

            return allUsers;
        }
    }
}
