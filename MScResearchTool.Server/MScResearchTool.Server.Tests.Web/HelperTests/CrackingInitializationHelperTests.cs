using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace MScResearchTool.Server.Tests.Web.HelperTests
{
    public class CrackingInitializationHelperTests
    {
        [Fact]
        public void ProcessFormFileToArray_FormFileInput_ReturnsByteArray()
        {

        }

        [Theory]
        [InlineData("pass", false)]
        [InlineData("passw0rd", true)]
        public void IsArchiveExtractable_MultiplePasswords_ActsProperly(string password, bool isExtractable) // IF false, RET false, else true
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var pathPrototype = Uri.UnescapeDataString(uri.Path);
            var path = Path.GetDirectoryName(pathPrototype);
        }
    }
}
