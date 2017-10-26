using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using System.Linq;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class IntegrationResultsBusiness : IIntegrationResultsBusiness
    {
        private IReportsBusiness _reportsBusiness { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }

        public IntegrationResultsBusiness
            (IReportsBusiness reportsBusiness,
            IIntegrationsBusiness integrationsBusiness,
            IIntegrationDistributionsBusiness integrationDistributionsBusiness)
        {
            _reportsBusiness = reportsBusiness;
            _integrationsBusiness = integrationsBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
        }

        public async Task ProcessResultAsync(IntegrationResult result)
        {
            if (result.Result.ToString() == "NaN")
                result.Result = 0.0;

            if (result.IsDistributed)
            {
                var eagerDistributions = await _integrationDistributionsBusiness.ReadAllEagerAsync();
                var eagerDistribution = eagerDistributions.FirstOrDefault(x => x.Id == result.Id);

                eagerDistribution.IsFinished = true;
                eagerDistribution.DeviceRAM = result.RAM;
                eagerDistribution.DeviceCPU = result.CPU;
                eagerDistribution.DeviceResult = result.Result;
                eagerDistribution.DeviceTime = result.ElapsedSeconds;

                var integration = await _integrationsBusiness.ReadByIdAsync(eagerDistribution.Task.Id);
                integration.PartialTime += result.ElapsedSeconds;
                integration.PartialResult += result.Result;

                await _integrationDistributionsBusiness.UpdateAsync(eagerDistribution);
                await _integrationsBusiness.UpdateAsync(integration);

                await VerifyCompletionAsync(integration.Id);
            }

            else
            {
                var integration = await _integrationsBusiness.ReadByIdAsync(result.Id);

                integration.IsFinished = true;
                integration.FullTime = result.ElapsedSeconds;
                integration.FullResult = result.Result;
                integration.DesktopRAM = result.RAM;
                integration.DesktopCPU = result.CPU;

                await _integrationsBusiness.UpdateAsync(integration);

                await VerifyCompletionAsync(result.Id);
            }
        }

        private async Task VerifyCompletionAsync(int mainTaskId)
        {
            var eagerIntegrations = await _integrationsBusiness.ReadAllEagerAsync();
            var eagerIntegration = eagerIntegrations.FirstOrDefault(x => x.Id == mainTaskId);

            if (eagerIntegration.IsFinished && eagerIntegration.Distributions.All(x => x.IsFinished))
            {
                await _reportsBusiness.GenerateIntegrationReportAsync(mainTaskId);
                await _integrationsBusiness.CascadeDeleteAsync(mainTaskId);
            }
        }
    }
}
