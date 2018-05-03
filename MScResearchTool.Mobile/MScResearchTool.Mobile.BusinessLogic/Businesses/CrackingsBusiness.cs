using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MScResearchTool.Mobile.BusinessLogic.Helper;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Domain.Models;

namespace MScResearchTool.Mobile.BusinessLogic.Businesses
{
    public class CrackingsBusiness : ICrackingsBusiness
    {
        private UnzippingHelper _unzippingHelper { get; set; }

        public CrackingsBusiness(UnzippingHelper unzippingHelper)
        {
            _unzippingHelper = unzippingHelper;
        }

        public async Task<CrackingResult> AttemptPasswordBreakingPasswordAsync(CrackingDistribution crackingDistributionTask)
        {
            CrackingResult result = null;

            await Task.Run(() =>
            {
                result = TryBreakArchive(crackingDistributionTask);

                result.Id = crackingDistributionTask.Id;
                result.IsDistributed = true;
            });

            return result;
        }

        private CrackingResult TryBreakArchive(CrackingDistribution crackingDistribution)
        {
            char[] range = SelectRange(crackingDistribution.AvailableCharacters, crackingDistribution.RangeBeginning, crackingDistribution.RangeEnding);

            var passwordFound = string.Empty;

            var watch = Stopwatch.StartNew();

            for(var i = 2; i <= 9; i++)
            {
                var generator = new SecretGenerator(crackingDistribution.AvailableCharacters, i);

                var secretAttempt = string.Empty;
                var isFound = false;

                while (secretAttempt != null)
                {
                    secretAttempt = generator.Next();

                    foreach (var item in range)
                    {
                        var test = item.ToString();
                        test += secretAttempt;

                        isFound = _unzippingHelper.IsArchiveExtractableWithPassword(crackingDistribution.ArchiveToCrack, test);

                        if (isFound)
                        {
                            passwordFound = test;
                            break;
                        }
                    }

                    if (isFound)
                    {
                        break;
                    }
                }

                if (isFound)
                {
                    break;
                }
            }

            watch.Stop();
            var elapsedMiliseconds = watch.ElapsedMilliseconds;
            var elapsedSeconds = (double)elapsedMiliseconds / 1000;

            var result = new CrackingResult
            {
                PasswordResult = passwordFound,
                ElapsedSeconds = elapsedSeconds
            };

            return result;
        }

        private char[] SelectRange(IList<char> availableCharacters, char rangeBeginning, char rangeEnding)
        {
            var startIndex = availableCharacters.IndexOf(rangeBeginning);
            var endIndex = availableCharacters.IndexOf(rangeEnding);

            IList<char> selectedRange = new List<char>();

            for(var i = startIndex; i <= endIndex; i++)
            {
                selectedRange.Add(availableCharacters[i]);
            }

            return selectedRange.ToArray();
        }
    }
}
