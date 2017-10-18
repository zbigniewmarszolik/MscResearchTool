using MScResearchTool.Server.Core.Helpers;
using System;
using System.Globalization;

namespace MScResearchTool.Server.Web.Helpers
{
    public class ParseDoubleHelper : IParseDoubleHelper
    {
        public double ParseInvariantCulture(string input)
        {
            var output = Double.Parse(input, CultureInfo.InvariantCulture);

            return output;
        }
    }
}
