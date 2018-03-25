using System;
using System.Linq;
using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class CrackingResultsBusiness : ICrackingResultsBusiness
    {
        private IReportsBusiness _reportsBusiness { get; set; }
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        private ICrackingDistributionsBusiness _crackingDistributionsBusiness { get; set; }

        public CrackingResultsBusiness
            (IReportsBusiness reportsBusiness,
            ICrackingsBusiness crackingsBusiness,
            ICrackingDistributionsBusiness crackingDistributionsBusiness)
        {
            _reportsBusiness = reportsBusiness;
            _crackingsBusiness = crackingsBusiness;
            _crackingDistributionsBusiness = crackingDistributionsBusiness;
        }

        public async Task ProcessResultAsync(CrackingResult result)
        {
            if(result == null)
            {
                throw new ArgumentNullException();
            }

            if(result.IsDistributed)
            {
                var eagerDistributions = await _crackingDistributionsBusiness.ReadAllEagerAsync();
                var eagerDistribution = eagerDistributions.FirstOrDefault(x => x.Id == result.Id);

                if(eagerDistribution == null)
                {
                    return;
                }

                else if(!eagerDistribution.ArchivePassword.Equals(result.PasswordResult))
                {
                    throw new ArgumentException();
                }

                else
                {
                    foreach(var item in eagerDistributions)
                    {
                        item.IsFinished = true;
                    }

                    eagerDistribution.IsFinished = true;
                    eagerDistribution.DeviceRAM = result.RAM;
                    eagerDistribution.DeviceCPU = result.CPU;
                    eagerDistribution.DeviceResult = result.PasswordResult;
                    eagerDistribution.DeviceTime = result.ElapsedSeconds;
                    eagerDistribution.BatteryUsage = result.BatteryUsage;
                    eagerDistribution.IsFounder = true;

                    var cracking = await _crackingsBusiness.ReadByIdAsync(eagerDistribution.Task.Id);
                    cracking.PartialTime = result.ElapsedSeconds;
                    cracking.FullResult = result.PasswordResult;

                    await _crackingDistributionsBusiness.UpdateAsync(eagerDistribution);
                    await _crackingsBusiness.UpdateAsync(cracking);

                    await SetDistributionsToDone();

                    await VerifyCompletionAsync(cracking.Id);
                }
            }

            else
            {
                var cracking = await _crackingsBusiness.ReadByIdAsync(result.Id);

                if (!cracking.ArchivePassword.Equals(result.PasswordResult))
                {
                    throw new ArgumentException();
                }

                cracking.IsFinished = true;
                cracking.FullTime = result.ElapsedSeconds;
                cracking.FullResult = result.PasswordResult;
                cracking.DesktopRAM = result.RAM;
                cracking.DesktopCPU = result.CPU;

                await _crackingsBusiness.UpdateAsync(cracking);

                await VerifyCompletionAsync(result.Id);
            }
        }

        private async Task VerifyCompletionAsync(int mainTaskId)
        {
            var eagerCrackings = await _crackingsBusiness.ReadAllEagerAsync();
            var eagerCracking = eagerCrackings.FirstOrDefault(x => x.Id == mainTaskId);

            if(eagerCracking.IsFinished && eagerCracking.Distributions.All(x => x.IsFinished))
            {
                await _reportsBusiness.GenerateCrackingReportAsync(mainTaskId);
                await _crackingsBusiness.CascadeDeleteAsync(mainTaskId);
            }
        }

        private async Task SetDistributionsToDone()
        {
            var distributions = await _crackingDistributionsBusiness.ReadAllEagerAsync();

            foreach(var item in distributions)
            {
                item.IsFinished = true;
                item.IsAvailable = false;

                await _crackingDistributionsBusiness.UpdateAsync(item);
            }
        }
    }
}
