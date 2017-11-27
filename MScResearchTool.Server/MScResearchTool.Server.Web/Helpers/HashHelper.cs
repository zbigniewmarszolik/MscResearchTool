using System;
using System.Security.Cryptography;
using System.Text;

namespace MScResearchTool.Server.Web.Helpers
{
    public class HashHelper
    {
        public string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string result = Convert.ToBase64String(salt);

            return result;
        }

        public string HashSequence(string inputSequence, string sequenceSalt)
        {
            var toHash = inputSequence + sequenceSalt;

            using (var sha256 = SHA256.Create())
            {
                var hashByteTable = sha256.ComputeHash(Encoding.UTF8.GetBytes(toHash));

                var hashed = Convert.ToBase64String(hashByteTable);

                return hashed;
            }
        }
    }
}
