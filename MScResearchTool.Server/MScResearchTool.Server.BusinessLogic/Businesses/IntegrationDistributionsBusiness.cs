using System.Collections.Generic;
using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using System.Linq;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class IntegrationDistributionsBusiness : IIntegrationDistributionsBusiness
    {
        private IIntegrationDistributionsRepository _integrationDistributionsRepository { get; set; }

        public IntegrationDistributionsBusiness(IIntegrationDistributionsRepository integrationDistributionsRepository)
        {
            _integrationDistributionsRepository = integrationDistributionsRepository;
        }

        public async Task<IList<IntegrationDistribution>> ReadAllEagerAsync()
        {
            IList<IntegrationDistribution> results = null;

            await Task.Run(() =>
            {
                results = _integrationDistributionsRepository.ReadEager();
            });

            return results;
        }

        public async Task<IList<IntegrationDistribution>> ReadAvailableAsync()
        {
            IList<IntegrationDistribution> resultSet = null;

            await Task.Run(() =>
            {
                resultSet = _integrationDistributionsRepository.ReadEager();
            });

            return resultSet.Where(x => x.IsAvailable && !x.IsFinished).ToList();
        }

        public async Task<IntegrationDistribution> ReadByIdAsync(int distributionId)
        {
            var singleResult = await ReadIntegrationDistributionsAsync();

            return singleResult.Where(x => x.Id == distributionId).First();
        }

        public async Task UpdateAsync(IntegrationDistribution integrationDistribution)
        {
            await Task.Run(() =>
            {
                _integrationDistributionsRepository.Update(integrationDistribution);
            });
        }

        public async Task UnstuckTakenAsync()
        {
            var set = await ReadIntegrationDistributionsAsync();
            var stuck = set.Where(x => x.IsFinished == false && x.IsAvailable == false).ToList();

            await Task.Run(() =>
            {
                foreach (var item in stuck)
                {
                    item.IsAvailable = true;
                    _integrationDistributionsRepository.Update(item);
                }
            });
        }

        public async Task UnstuckByIdAsync(int distributionId)
        {
            var distribution = await ReadByIdAsync(distributionId);

            await Task.Run(() =>
            {
                if(!distribution.IsAvailable)
                {
                    distribution.IsAvailable = true;
                    _integrationDistributionsRepository.Update(distribution);
                }
            });
        }

        private async Task<IList<IntegrationDistribution>> ReadIntegrationDistributionsAsync()
        {
            IList<IntegrationDistribution> results = null;

            await Task.Run(() =>
            {
                results = _integrationDistributionsRepository.Read();
            });

            foreach(var item in results)
            {
                item.Task = null;
            }

            return results;
        }
    }
}
