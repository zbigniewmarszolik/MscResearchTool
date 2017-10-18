using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using System.Collections.Generic;
using MScResearchTool.Server.Core.Factories;
using System.Linq;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class IntegrationTasksBusiness : IIntegrationTasksBusiness
    {
        private IIntegrationTasksRepository _integrationTasksRepository { get; set; }
        private IIntegrationDistributionFactory _integrationDistributionFactory { get; set; }

        public IntegrationTasksBusiness
            (IIntegrationTasksRepository integrationTasksRepository,
            IIntegrationDistributionFactory integrationDistributionFactory)
        {
            _integrationTasksRepository = integrationTasksRepository;
            _integrationDistributionFactory = integrationDistributionFactory;
        }

        public async Task CascadeDeleteAsync(int taskId)
        {
            await Task.Run(() =>
            {
                _integrationTasksRepository.Delete(taskId);
            });
        }

        public async Task UpdateIntegrationTaskAsync(IntegrationTask integrationTask)
        {
            await Task.Run(() =>
            {
                _integrationTasksRepository.Update(integrationTask);
            });
        }

        public async Task DistributeAndPersistAsync(IntegrationTask integrationTask)
        {
            await Task.Run(() =>
            {
                integrationTask.Distributions = DistributeIntegrations(integrationTask);

                _integrationTasksRepository.Create(integrationTask);
            });
        }

        public async Task<IList<IntegrationTask>> ReadAllIntegrationTasksAsync()
        {
            return await ReadIntegrationTasks();
        }

        public async Task<IList<IntegrationTask>> ReadAvailableFullIntegrationsAsync()
        {
            var resultSet = await ReadIntegrationTasks();

            return resultSet.Where(x => !x.IsTaken).ToList();
        }

        public async Task<IntegrationTask> ReadByIdAsync(int taskId)
        {
            var singleResult = await ReadIntegrationTasks();

            return singleResult.Where(x => x.Id == taskId).First();
        }

        private async Task<IList<IntegrationTask>> ReadIntegrationTasks()
        {
            IList<IntegrationTask> results = null;

            await Task.Run(() =>
            {
                results = _integrationTasksRepository.Read();
            });

            foreach(var item in results)
            {
                item.Distributions = null;
            }

            return results;
        }

        private IList<IntegrationDistribution> DistributeIntegrations(IntegrationTask task)
        {
            IList<IntegrationDistribution> distributions = new List<IntegrationDistribution>();

            var numberOfElements = (task.UpBoundary - task.DownBoundary) * task.Accuracy;

            var down = 0.0;
            var up = 0.0;

            for (int i = 0; i < task.DroidIntervals; i++)
            {
                down = i * numberOfElements / task.DroidIntervals / task.Accuracy + task.DownBoundary;
                up = (i + 1) * numberOfElements / task.DroidIntervals / task.Accuracy + task.DownBoundary;

                if (i == 0)
                {
                    down = task.DownBoundary;
                }

                if(i == task.DroidIntervals - 1)
                {
                    up = task.UpBoundary;
                }

                var dist = _integrationDistributionFactory.GetInstance(task, down, up);

                distributions.Add(dist);
            }

            return distributions;
        }
    }
}
