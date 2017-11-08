using MScResearchTool.Server.Core.Types;
using NCalc;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MScResearchTool.Server.Web.Helpers
{
    public class IntegralInitializationHelper
    {
        public bool IsFormulaCorrectForCSharp(string integrationFormula)
        {
            object expResult = null;

            var formulaWithFixedChars = ReplaceWrongChars(integrationFormula);
            var formulaWithAssignedVariable = formulaWithFixedChars.Replace("x", "1");

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

        public bool AreConstraintsCorrect(double upperLimit, double lowerLimit, int precision)
        {
            if (precision < 1)
                return false;

            if (lowerLimit >= upperLimit)
                return false;

            return true;
        }

        public bool IsForTrapezoidIntegration(string integrationMethod)
        {
            if (integrationMethod.Equals(ETaskType.Square_integration.ToString()))
                return false;

            else return true;
        }

        public string PrepareFormulaForExpression(string originalFormula)
        {
            return ReplaceWrongChars(originalFormula);
        }

        private string ReplaceWrongChars(string formula)
        {
            if (formula.Contains("sin"))
                formula = formula.Replace("sin", "Sin");

            if (formula.Contains("asin"))
                formula = formula.Replace("asin", "Asin");

            if (formula.Contains("aSin"))
                formula = formula.Replace("aSin", "Asin");

            if (formula.Contains("arcsin"))
                formula = formula.Replace("arcsin", "Asin");

            if (formula.Contains("arcSin"))
                formula = formula.Replace("arcSin", "Asin");

            if (formula.Contains("cos"))
                formula = formula.Replace("cos", "Cos");

            if (formula.Contains("acos"))
                formula = formula.Replace("acos", "Acos");

            if (formula.Contains("aCos"))
                formula = formula.Replace("aCos", "Acos");

            if (formula.Contains("arccos"))
                formula = formula.Replace("arccos", "Acos");

            if (formula.Contains("arcCos"))
                formula = formula.Replace("arcCos", "Acos");

            if (formula.Contains("tan"))
                formula = formula.Replace("tan", "Tan");

            if (formula.Contains("tg"))
                formula = formula.Replace("tg", "Tan");

            if (formula.Contains("atan"))
                formula = formula.Replace("atan", "Atan");

            if (formula.Contains("aTan"))
                formula = formula.Replace("aTan", "Atan");

            if (formula.Contains("arctan"))
                formula = formula.Replace("arctan", "Atan");

            if (formula.Contains("arcTan"))
                formula = formula.Replace("arcTan", "Atan");

            if (formula.Contains("arctg"))
                formula = formula.Replace("arctg", "Atan");

            if (formula.Contains("sqrt"))
                formula = formula.Replace("sqrt", "Sqrt");

            if (formula.Contains("exp"))
                formula = formula.Replace("exp", "Exp");

            if (formula.Contains("e"))
                formula = formula.Replace("e", "Exp");

            if (formula.Contains("pow"))
                formula = formula.Replace("pow", "Pow");

            int counter = 0;

            while (formula.Contains("ln"))
            {
                formula = DefineNaturalLogarithm(formula, counter);
                counter++;
            }

            if (formula.Contains("lg"))
                formula = formula.Replace("lg", "Log10");

            if (formula.Contains("log"))
                formula = formula.Replace("log", "Log");

            return formula;
        }

        private string DefineNaturalLogarithm(string formula, int stage)
        {
            var tempFormula = "";
            var regex = new Regex(Regex.Escape("ln"));

            if (stage > 0)
            {
                foreach (var item in formula)
                    tempFormula += item;

                tempFormula = tempFormula.Replace("ln", "Log!");
            }

            formula = regex.Replace(formula, "Log", 1);

            if (stage == 0)
            {
                foreach (var item in formula)
                    tempFormula += item;

                tempFormula = tempFormula.Replace("Log", "Log!");
            }

            var index = tempFormula.IndexOf("!");
            var bracket = tempFormula.IndexOf(")", index);

            StringBuilder newFormula = new StringBuilder(formula);
            newFormula.Remove(bracket - 1, 1);

            newFormula.Insert(bracket - 1, ", ");
            newFormula = newFormula.Replace(",", "!");
            newFormula.Insert(bracket + 1, Math.E + ")");
            newFormula = newFormula.Replace(",", ".");
            newFormula = newFormula.Replace("!", ",");

            var build = newFormula.ToString();

            return build;
        }
    }
}