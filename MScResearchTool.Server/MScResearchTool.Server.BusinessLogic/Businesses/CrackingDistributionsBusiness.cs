using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class CrackingDistributionsBusiness : ICrackingDistributionsBusiness
    {
        private ICrackingDistributionsRepository _crackingDistributionsRepository { get; set; }

        public CrackingDistributionsBusiness(ICrackingDistributionsRepository crackingDistributionsRepository)
        {
            _crackingDistributionsRepository = crackingDistributionsRepository;
        }

        public async Task<IList<CrackingDistribution>> ReadAllEagerAsync()
        {
            IList<CrackingDistribution> results = null;

            await Task.Run(() =>
            {
                results = _crackingDistributionsRepository.ReadEager();
            });

            return results;
        }

        public async Task<IList<CrackingDistribution>> ReadAvailableAsync()
        {
            IList<CrackingDistribution> resultSet = null;

            await Task.Run(() =>
            {
                resultSet = _crackingDistributionsRepository.ReadEager();
            });

            return resultSet.Where(x => x.IsAvailable && !x.IsFinished).ToList();
        }

        public async Task<CrackingDistribution> ReadByIdAsync(int distributionId)
        {
            IList<CrackingDistribution> results = null;

            await Task.Run(() =>
            {
                results = _crackingDistributionsRepository.Read();
            });

            return results.Where(x => x.Id == distributionId).First();
        }

        public async Task UpdateAsync(CrackingDistribution crackingDistribution)
        {
            await Task.Run(() =>
            {
                _crackingDistributionsRepository.Update(crackingDistribution);
            });
        }

        public async Task UnstuckTakenAsync()
        {
            var set = _crackingDistributionsRepository.ReadEager();
            var stuck = set.Where(x => x.IsFinished == false && x.IsAvailable == false).ToList();

            await Task.Run(() =>
            {
                foreach (var item in stuck)
                {
                    item.IsAvailable = true;
                    _crackingDistributionsRepository.Update(item);
                }
            });
        }

        public async Task UnstuckByIdAsync(int crackingId)
        {
            IList<CrackingDistribution> distributions = null;
            IList<CrackingDistribution> correspondingDistributions = null;

            await Task.Run(() =>
            {
                distributions = _crackingDistributionsRepository.ReadEager();

                correspondingDistributions = distributions.Where(x => x.Task.Id == crackingId).ToList();
            });

            await Task.Run(() =>
            {
                foreach (var item in correspondingDistributions)
                {
                    if (!item.IsAvailable && !item.IsFinished)
                    {
                        item.IsAvailable = true;
                        _crackingDistributionsRepository.Update(item);
                    }
                }
            });
        }
    }
}
