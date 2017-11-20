using MScResearchTool.Server.Web.Helpers;
using Xunit;

namespace MScResearchTool.Server.Tests.Web.HelperTests
{
    public class IntegralInitializationHelperTests
    {
        private IntegralInitializationHelper _integralInitializationHelper { get; set; }

        public IntegralInitializationHelperTests()
        {
            _integralInitializationHelper = new IntegralInitializationHelper();
        }

        [Theory]
        [InlineData("Trapezoid_integration", true)]
        [InlineData("Square_integration", false)]
        public void IsForTrapezoidIntegration_TrueOrFalse_ProperlyAssigning(string input, bool condition)
        {
            var result = _integralInitializationHelper.IsForTrapezoidIntegration(input);

            Assert.Equal(condition, result);
        }

        [Theory]
        [InlineData(10.0, 5.0, 15, true)]
        [InlineData(10.0, 9.90, 1, true)]
        [InlineData(10.0, 5.0, -4, false)]
        [InlineData(10.0, 15.0, 0, false)]
        [InlineData(10.0, 25.5, 99, false)]
        public void AreConstraintsCorrect_MultipleInputs_ProperlyReturning(double upperLimit, double lowerLimit, int precision, bool condition)
        {
            var result = _integralInitializationHelper.AreConstraintsCorrect(upperLimit, lowerLimit, precision);

            Assert.Equal(condition, result);
        }

        [Theory]
        [InlineData("x+5-10*x*x", true)]
        [InlineData("15+30*45/2+68*x", true)]
        [InlineData("2*y+15", false)]
        [InlineData("sin(x)+cos(x*x-2)-tg(3*x*x)", true)]
        [InlineData("1-arccos(x+2)-pow(3,4)", true)]
        [InlineData("ln(x+5)*3-2", true)]
        [InlineData("x+5-10*x*x*x-adsdsdsadsadsadsadsa", false)]
        public void IsFormulaCorrectForCSharp_MultipleInputs_ProperlyTransformingFormula(string value, bool condition)
        {
            var result = _integralInitializationHelper.IsFormulaCorrectForCSharp(value);

            Assert.Equal(condition, result);
        }

        [Fact]
        public void PrepareFormulaForExpression_StringInput_StringOutput()
        {
            var res = _integralInitializationHelper.PrepareFormulaForExpression("x+2");

            Assert.IsType<string>(res);
        }
    }
}
