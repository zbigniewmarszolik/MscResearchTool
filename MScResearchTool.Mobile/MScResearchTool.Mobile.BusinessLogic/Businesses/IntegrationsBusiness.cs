using System.Threading.Tasks;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Domain.Models;
using System.Diagnostics;
using NCalc;

namespace MScResearchTool.Mobile.BusinessLogic.Businesses
{
    public class IntegrationsBusiness : IIntegrationsBusiness
    {
        public async Task<IntegrationResult> CalculateIntegrationAsync(IntegrationDistribution integrationDistributionTask)
        {
            IntegrationResult result = null;

            await Task.Run(() =>
            {
                if (integrationDistributionTask.IsTrapezoidMethodRequested)
                    result = IntegrateByTrapezoids(integrationDistributionTask);

                else result = IntegrateBySquares(integrationDistributionTask);

                result.Id = integrationDistributionTask.Id;
                result.IsDistributed = true;
            });

            return result;
        }

        private IntegrationResult IntegrateBySquares(IntegrationDistribution integrationDistributionTask)
        {
            var watch = Stopwatch.StartNew();

            var a = integrationDistributionTask.DownBoundary;
            var b = integrationDistributionTask.UpBoundary;
            var n = integrationDistributionTask.Accuracy;

            var h = 0.0;
            var resultValue = 0.0;

            h = (b - a) / n;

            for (int i = 1; i <= n; i++)
            {
                resultValue += Function(a + i * h, integrationDistributionTask.Formula) * h;
            }

            watch.Stop();
            var elapsedMiliSeconds = watch.ElapsedMilliseconds;
            var elapsedSeconds = (double)elapsedMiliSeconds / 1000;

            var result = new IntegrationResult
            {
                Result = resultValue,
                ElapsedSeconds = elapsedSeconds
            };

            return result;
        }

        private IntegrationResult IntegrateByTrapezoids(IntegrationDistribution integrationDistributionTask)
        {
            var watch = Stopwatch.StartNew();

            var a = integrationDistributionTask.DownBoundary;
            var b = integrationDistributionTask.UpBoundary;
            var n = integrationDistributionTask.Accuracy;

            var h = 0.0;
            var resultValue = 0.0;

            h = (b - a) / n;

            for (int i = 1; i <= n; i++)
            {
                resultValue += Function(a + i * h, integrationDistributionTask.Formula);
            }

            resultValue += Function(a, integrationDistributionTask.Formula) / 2;
            resultValue += Function(b, integrationDistributionTask.Formula) / 2;
            resultValue *= h;

            watch.Stop();
            var elapsedMiliSeconds = watch.ElapsedMilliseconds;
            var elapsedSeconds = (double)elapsedMiliSeconds / 1000;

            var result = new IntegrationResult
            {
                Result = resultValue,
                ElapsedSeconds = elapsedSeconds
            };

            return result;
        }

        private double Function(double localX, string formula)
        {
            var localXString = localX.ToString();
            localXString = localXString.Replace(",", ".");
            var equation = formula.Replace("x", localXString);

            Expression exp = new Expression(equation);
            var res = exp.Evaluate();

            return (double)res;
        }
    }
}
