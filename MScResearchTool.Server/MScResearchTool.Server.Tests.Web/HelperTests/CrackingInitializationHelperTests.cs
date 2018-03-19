using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Http;
using Moq;
using MScResearchTool.Server.Web.Helpers;
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
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;

            var result = new CrackingInitializationHelper().ProcessFormFileToArray(file);

            Assert.IsType<byte[]>(result);
        }

        [Theory]
        [InlineData("pass", false)]
        [InlineData("passw0rd", true)]
        public void IsArchiveExtractable_MultiplePasswords_ActsProperly(string password, bool isExtractable) // IF false, RET false, else true
        {
            var pass = "passw0rd";
            var content = "entry_content";

            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var pathPrototype = Uri.UnescapeDataString(uri.Path);
            var path = Path.GetDirectoryName(pathPrototype);

            var memoryStream = new MemoryStream();

            var outputMemoryStream = new MemoryStream();

            var zipStream = new ZipOutputStream(outputMemoryStream);
            zipStream.SetLevel(3);
            zipStream.Password = pass;

            var newEntry = new ZipEntry(content);
            newEntry.DateTime = DateTime.Now;

            zipStream.PutNextEntry(newEntry);

            StreamUtils.Copy(memoryStream, zipStream, new byte[4096]);
            zipStream.CloseEntry();
            zipStream.IsStreamOwner = false;
            zipStream.Close();

            var outArray = outputMemoryStream.ToArray();

            var helper = new CrackingInitializationHelper();
            var result = helper.IsArchiveExtractable(outArray, password);

            Assert.Equal(isExtractable, result);
            Assert.Equal(content, helper.SerializedContent);
        }
    }
}
