using MScResearchTool.Server.Core.Models;
using System;

namespace MScResearchTool.Server.Web.Factories
{
    public class IntegrationFactory
    {
        public Integration GetInstance(int intervals, double upperBound, double lowerBound, int precision, string processedFormula, string originalInputFormula, bool isTrapezoidMethod)
        {
            var instance = new Integration()
            {
                CreationDate = DateTime.Now,
                Accuracy = precision,
                DroidIntervals = intervals,
                UpBoundary = upperBound,
                DownBoundary = lowerBound,
                Formula = processedFormula,
                UnresolvedFormula = originalInputFormula,
                IsTrapezoidMethodRequested = isTrapezoidMethod,
                IsFinished = false,
                IsAvailable = true
            };

            return instance;
        }
    }
}
