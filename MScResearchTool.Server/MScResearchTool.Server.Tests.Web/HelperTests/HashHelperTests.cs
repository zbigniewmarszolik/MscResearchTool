using MScResearchTool.Server.Web.Helpers;
using System;
using System.Security.Cryptography;
using Xunit;

namespace MScResearchTool.Server.Tests.Web.HelperTests
{
    public class HashHelperTests
    {
        private HashHelper _hashHelper { get; set; }

        public HashHelperTests()
        {
            _hashHelper = new HashHelper();
        }

        [Fact]
        public void HashSequence_ForStrings_HashingInputProperly()
        {
            var sequence = "p@ssw0rd";
            var salt = GenerateUniqueSalt();

            var result1 = _hashHelper.HashSequence(sequence, salt);

            Assert.NotNull(result1);

            var result2 = _hashHelper.HashSequence(sequence, salt);

            Assert.Equal(result1, result2);

            salt = GenerateUniqueSalt();

            var result3 = _hashHelper.HashSequence(sequence, salt);

            Assert.NotEqual(result1, result3);
        }

        [Fact]
        public void GenerateSalt_ForStrings_GeneratingUniqueSalt()
        {
            var salt1 = _hashHelper.GenerateSalt();
            var salt2 = _hashHelper.GenerateSalt();

            Assert.NotNull(salt1);
            Assert.NotNull(salt2);

            Assert.NotEqual(salt1, salt2);
        }

        private string GenerateUniqueSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string result = Convert.ToBase64String(salt);

            return result;
        }
    }
}
