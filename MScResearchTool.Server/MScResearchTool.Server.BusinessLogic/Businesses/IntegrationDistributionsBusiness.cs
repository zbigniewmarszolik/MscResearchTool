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

        public async Task UpdateIntegrationDistributionAsync(IntegrationDistribution integrationDistribution)
        {
            await Task.Run(() =>
            {
                _integrationDistributionsRepository.Update(integrationDistribution);
            });
        }

        public async Task<IList<IntegrationDistribution>> ReadAvailableIntegrationDistributionsAsync()
        {
            var resultSet = await ReadIntegrationDistributions();

            return resultSet.Where(x => !x.IsTaken).ToList();
        }

        public async Task<IntegrationDistribution> ReadByIdAsync(int distributionId)
        {
            var singleResult = await ReadIntegrationDistributions();

            return singleResult.Where(x => x.Id == distributionId).First();
        }

        private async Task<IList<IntegrationDistribution>> ReadIntegrationDistributions()
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
