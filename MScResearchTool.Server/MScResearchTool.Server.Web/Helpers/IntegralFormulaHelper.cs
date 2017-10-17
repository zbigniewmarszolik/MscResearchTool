using MScResearchTool.Server.Core.Types;
using NCalc;
using System;

namespace MScResearchTool.Server.Web.Helpers
{
    public class IntegralFormulaHelper
    {
        public bool IsFormulaCorrectForCSharp(string integrationFormula)
        {
            object expResult = null;

            var formulaWithFixedMathLetters = ReplaceSmallMathLetters(integrationFormula);
            var formulaWithAssignedVariable = formulaWithFixedMathLetters.Replace("x", "1");

            try
            {
                Expression exp = new Expression(formulaWithAssignedVariable);
                expResult = exp.Evaluate();
            }
            catch (Exception e)
            {
                return false;
            }

            if (expResult != null)
                return true;

            else return false;
        }

        public bool IsForTrapezoidIntegration(string integrationMethod)
        {
            if (integrationMethod.Equals(ETaskType.Square_integration.ToString()))
                return false;

            else return true;
        }

        private string ReplaceSmallMathLetters(string formula)
        {
            if (formula.Contains("sin"))
                formula = formula.Replace("sin", "Sin");

            return formula;
        }
    }
}
