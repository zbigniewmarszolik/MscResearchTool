namespace MScResearchTool.Server.Core.Helpers
{
    public interface IIntegralInitializationHelper
    {
        bool IsFormulaCorrectForCSharp(string integrationFormula);
        bool AreConstraintsCorrect(double upperLimit, double lowerLimit, int precision);
        bool IsForTrapezoidIntegration(string integrationMethod);
    }
}
