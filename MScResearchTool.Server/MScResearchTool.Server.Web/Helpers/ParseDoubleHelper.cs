using System;
using System.Globalization;

namespace MScResearchTool.Server.Web.Helpers
{
    public class ParseDoubleHelper
    {
        public double ParseInvariantCulture(string input)
        {
            var output = Double.Parse(input, CultureInfo.InvariantCulture);

            return output;
        }

        public double ParseEulerNumber(double e)
        {
            var input = e.ToString();
            input = input.Replace(",", ".");

            return ParseInvariantCulture(input);
        }
    }
}
