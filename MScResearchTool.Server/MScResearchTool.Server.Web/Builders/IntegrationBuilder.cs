using MScResearchTool.Server.Core.Models;
using System;

namespace MScResearchTool.Server.Web.Builders
{
    public class IntegrationBuilder
    {
        public int IntegrationPrecision { get; set; }
        public int NumberOfDistributions { get; set; }
        public double UpperLimit { get; set; }
        public double LowerLimit { get; set; }
        public string IntegrationFormula { get; set; }
        public string UnmodifiedInputFormula { get; set; }
        public bool IsTrapezoidMethodRequired { get; set; }

        public Integration Build()
        {
            var builtIntegration = new Integration()
            {
                CreationDate = DateTime.Now,
                IsFinished = false,
                IsAvailable = true,
                IsResultNaN = false,
                Accuracy = IntegrationPrecision,
                DroidIntervals = NumberOfDistributions,
                UpBoundary = UpperLimit,
                DownBoundary = LowerLimit,
                Formula = IntegrationFormula,
                UnresolvedFormula = UnmodifiedInputFormula,
                IsTrapezoidMethodRequested = IsTrapezoidMethodRequired
            };

            return builtIntegration;
        }
    }
}
