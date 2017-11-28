using MScResearchTool.AccountCreator.Domain.Models;
using MScResearchTool.AccountCreator.Domain.Services;
using System;

namespace MScResearchTool.AccountCreator.Application.Managers
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
                Console.WriteLine();
                Console.WriteLine("Processing request...");
                Console.WriteLine();

                _usersService.PostUserAsync(user).Wait();

                Console.WriteLine("Account " + username + " created succesfully.");
                Console.WriteLine("Press ENTER to exit.");
                Console.Read();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: " + exc.InnerException.Message);
                Console.WriteLine("Press ENTER to exit.");
                Console.Read();
            }
        }
    }
}
