using MScResearchTool.Server.Core.Models;
using System.Linq;
using Xunit;

namespace MScResearchTool.Server.Tests.Model.ModelTests
{
    public class CrackingCharactersTests
    {
        private string _expectedCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        [Fact]
        public void Instance_Get_CorrectAmountOfCharacters()
        {
            var instance = CrackingCharacters.Instance().Characters;

            Assert.Equal(62, instance.Count);
        }

        [Fact]
        public void Instance_Get_CharactersAreCorrect()
        {
            var setOfCharacters = CrackingCharacters.Instance().Characters;

            var serializedCharacters = string.Empty;

            foreach(var item in setOfCharacters)
            {
                serializedCharacters += item;
            }

            Assert.Equal(_expectedCharacters, serializedCharacters);
        }
    }
}
