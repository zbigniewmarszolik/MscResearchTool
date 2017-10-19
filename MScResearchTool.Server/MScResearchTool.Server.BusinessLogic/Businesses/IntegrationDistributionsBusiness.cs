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

        public async Task<IList<IntegrationDistribution>> ReadAvailableAsync()
        {
            var resultSet = await ReadIntegrationDistributionsAsync();

            return resultSet.Where(x => !x.IsTaken && !x.IsFinished).ToList();
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
            var stuck = set.Where(x => x.IsFinished == false && x.IsTaken == true).ToList();

            await Task.Run(() =>
            {
                foreach (var item in stuck)
                {
                    item.IsTaken = false;
                    _integrationDistributionsRepository.Update(item);
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
