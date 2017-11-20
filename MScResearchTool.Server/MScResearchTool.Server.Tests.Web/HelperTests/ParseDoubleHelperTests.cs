using MScResearchTool.Server.Web.Helpers;
using Xunit;

namespace MScResearchTool.Server.Tests.Web.HelperTests
{
    public class ParseDoubleHelperTests
    {
        private ParseDoubleHelper _parseDoubleHelper { get; set; }

        public ParseDoubleHelperTests()
        {
            _parseDoubleHelper = new ParseDoubleHelper();
        }

        [Theory]
        [InlineData("15.0")]
        [InlineData("4,1")]
        [InlineData("22")]
        [InlineData("113,4598")]
        public void ParseInvariantCulture_StringNumbers_ParsingToDouble(string value)
        {
            var result = _parseDoubleHelper.ParseInvariantCulture(value);

            Assert.IsType<double>(result);
        }
    }
}
