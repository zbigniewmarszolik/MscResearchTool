using MScResearchTool.AccountCreator.Domain.Models;
using MScResearchTool.AccountCreator.Domain.Services;
using System;

namespace MScResearchTool.AccountCreator.Application
{
    public class AccountManager
    {
        private IUsersService _usersService { get; set; }

        public AccountManager(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public void RunApp()
        {
            Console.WriteLine("Please enter new username and press <ENTER>");
            var username = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter new password and press <ENTER>");
            var password = Console.ReadLine();
            Console.WriteLine();

            var user = new User
            {
                Name = username,
                Password = password
            };

            try
            {
                _usersService.PostUser(user);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                return;
            }

            Console.WriteLine("Account " + username + " created succesfully.");
        }
    }
}
