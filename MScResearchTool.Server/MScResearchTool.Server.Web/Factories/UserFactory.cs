using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.Helpers;

namespace MScResearchTool.Server.Web.Factories
{
    public class UserFactory
    {
        private HashHelper _hashHelper { get; set; }

        public UserFactory(HashHelper hashHelper) => _hashHelper = hashHelper;

        public User Create(string username, string correctPassword)
        {
            var salt = _hashHelper.GenerateSalt();
            var cipher = _hashHelper.HashSequence(correctPassword, salt);

            var user = new User
            {
                Name = username,
                Password = cipher,
                Salt = salt
            };

            return user;
        }
    }
}
