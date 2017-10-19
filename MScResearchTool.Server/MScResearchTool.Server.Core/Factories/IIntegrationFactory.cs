using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Core.Factories
{
    public interface IIntegrationFactory
    {
        Integration GetInstance(int intervals, double upperBound, double lowerBound, int precision, string formula, bool isTrapezoidMethod);
    }
}
