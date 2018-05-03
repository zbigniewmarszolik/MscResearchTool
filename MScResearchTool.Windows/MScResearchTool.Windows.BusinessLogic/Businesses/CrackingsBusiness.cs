﻿using System.Diagnostics;
using System.Threading.Tasks;
using MScResearchTool.Windows.BusinessLogic.Helpers;
using MScResearchTool.Windows.Domain.Businesses;
using MScResearchTool.Windows.Domain.Models;

namespace MScResearchTool.Windows.BusinessLogic.Businesses
{
    public class CrackingsBusiness : ICrackingsBusiness
    {
        private HardwareInfoHelper _hardwareInfoHelper { get; set; }
        private UnzippingHelper _unzippingHelper { get; set; }

        public CrackingsBusiness(HardwareInfoHelper hardwareInfoHelper, UnzippingHelper unzippingHelper)
        {
            _hardwareInfoHelper = hardwareInfoHelper;
            _unzippingHelper = unzippingHelper;
        }

        public async Task<CrackingResult> BreakPasswordAsync(Cracking crackingTask)
        {
            CrackingResult result = null;

            await Task.Run(() =>
            {
                result = BreakArchive(crackingTask);

                result.Id = crackingTask.Id;
                result.IsDistributed = false;
                result.CPU = _hardwareInfoHelper.GetCPUDetails();
                result.RAM = _hardwareInfoHelper.GetRAMAmountInMB();
            });

            return result;
        }

        private CrackingResult BreakArchive(Cracking crackingTask)
        {
            var passwordFound = string.Empty;

            var watch = Stopwatch.StartNew();

            for (var i = 3; i <= 10; i++)
            {
                var generator = new SecretGenerator(crackingTask.AvailableCharacters, i);

                var secretAttempt = string.Empty;
                var isFound = false;

                while (secretAttempt != null)
                {
                    secretAttempt = generator.Next();

                    isFound = _unzippingHelper.IsArchiveExtractableWithPassword(crackingTask.ArchiveToCrack, secretAttempt);

                    if (isFound)
                    {
                        break;
                    }
                }

                if (isFound)
                {
                    passwordFound = secretAttempt;
                    break;
                }
            }

            watch.Stop();
            var elapsedMiliSeconds = watch.ElapsedMilliseconds;
            var elapsedSeconds = (double)elapsedMiliSeconds / 1000;

            var result = new CrackingResult
            {
                PasswordResult = passwordFound,
                ElapsedSeconds = elapsedSeconds
            };

            return result;
        }
    }
}
