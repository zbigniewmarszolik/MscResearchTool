using System.Threading.Tasks;
using MScResearchTool.Windows.Domain.Businesses;
using MScResearchTool.Windows.Domain.Models;
using System.Diagnostics;
using NCalc;
using MScResearchTool.Windows.BusinessLogic.Helpers;

namespace MScResearchTool.Windows.BusinessLogic.Businesses
{
    public class IntegrationsBusiness : IIntegrationsBusiness
    {
        private HardwareInfoHelper _hardwareInfoHelper { get; set; }

        public IntegrationsBusiness(HardwareInfoHelper hardwareInfoHelper)
        {
            _hardwareInfoHelper = hardwareInfoHelper;
        }

        public async Task<IntegrationResult> CalculateIntegrationAsync(Integration integrationTask)
        {
            IntegrationResult result = null;

            await Task.Run(() =>
            {
                if (integrationTask.IsTrapezoidMethodRequested)
                    result = IntegrateByTrapezoids(integrationTask);

                else result = IntegrateBySquares(integrationTask);

                result.Id = integrationTask.Id;
                result.IsDistributed = false;
                result.CPU = _hardwareInfoHelper.GetCPUDetails();
                result.RAM = _hardwareInfoHelper.GetRAMAmountInMB();
            });

            return result;
        }

        private IntegrationResult IntegrateBySquares(Integration integrationTask)
        {
            var watch = Stopwatch.StartNew();

            var a = integrationTask.DownBoundary;
            var b = integrationTask.UpBoundary;
            var n = integrationTask.Accuracy;

            var h = 0.0;
            var resultValue = 0.0;

            h = (b - a) / n;

            for (int i = 1; i <= n; i++)
            {
                resultValue += Function(a + i * h, integrationTask.Formula) * h;
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

        private IntegrationResult IntegrateByTrapezoids(Integration integrationTask)
        {
            var watch = Stopwatch.StartNew();

            var a = integrationTask.DownBoundary;
            var b = integrationTask.UpBoundary;
            var n = integrationTask.Accuracy;

            var h = 0.0;
            var resultValue = 0.0;

            h = (b - a) / n;

            for (int i = 1; i <= n; i++)
            {
                resultValue += Function(a + i * h, integrationTask.Formula);
            }

            resultValue += Function(a, integrationTask.Formula) / 2;
            resultValue += Function(b, integrationTask.Formula) / 2;
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
