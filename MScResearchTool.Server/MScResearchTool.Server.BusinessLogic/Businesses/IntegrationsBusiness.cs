﻿using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using MScResearchTool.Server.BusinessLogic.Factories;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class IntegrationsBusiness : IIntegrationsBusiness
    {
        private IIntegrationsRepository _integrationsRepository { get; set; }
        private IntegrationDistributionFactory _integrationDistributionFactory { get; set; }

        public IntegrationsBusiness
            (IIntegrationsRepository integrationsRepository,
            IntegrationDistributionFactory integrationDistributionFactory)
        {
            _integrationsRepository = integrationsRepository;
            _integrationDistributionFactory = integrationDistributionFactory;
        }

        public async Task DistributeAndPersistAsync(Integration integrationTask)
        {
            await Task.Run(async () =>
            {
                integrationTask.Distributions =  await DistributeIntegrationsAsync(integrationTask);

                _integrationsRepository.Create(integrationTask);
            });
        }

        public async Task<IList<Integration>> ReadAllAsync()
        {
            return await ReadIntegrationTasksAsync();
        }

        public async Task<IList<Integration>> ReadAllEagerAsync()
        {
            IList<Integration> results = null;

            await Task.Run(() =>
            {
                results = _integrationsRepository.ReadEager();
            });

            return results;
        }

        public async Task<IList<Integration>> ReadAvailableAsync()
        {
            var resultSet = await ReadIntegrationTasksAsync();

            return resultSet.Where(x => x.IsAvailable && !x.IsFinished).ToList();
        }

        public async Task<Integration> ReadByIdAsync(int taskId)
        {
            var singleResult = await ReadIntegrationTasksAsync();

            return singleResult.Where(x => x.Id == taskId).First();
        }

        public async Task UpdateAsync(Integration integrationTask)
        {
            await Task.Run(() =>
            {
                _integrationsRepository.Update(integrationTask);
            });
        }

        public async Task UnstuckTakenAsync()
        {
            var set = await ReadIntegrationTasksAsync();
            var stuck = set.Where(x => x.IsFinished == false && x.IsAvailable == false).ToList();

            await Task.Run(() =>
            {
                foreach (var item in stuck)
                {
                    item.IsAvailable = true;
                    _integrationsRepository.Update(item);
                }
            });
        }

        public async Task UnstuckByIdAsync(int taskId)
        {
            var task = await ReadByIdAsync(taskId);

            await Task.Run(() =>
            {
                if(!task.IsAvailable)
                {
                    task.IsAvailable = true;
                    _integrationsRepository.Update(task);
                }
            });
        }

        public async Task CascadeDeleteAsync(int taskId)
        {
            await Task.Run(() =>
            {
                _integrationsRepository.Delete(taskId);
            });
        }

        private async Task<IList<IntegrationDistribution>> DistributeIntegrationsAsync(Integration task)
        {
            IList<IntegrationDistribution> distributions = new List<IntegrationDistribution>();

            var numberOfElements = (task.UpBoundary - task.DownBoundary) * task.Accuracy;

            var down = 0.0;
            var up = 0.0;

            await Task.Run(() =>
            {
                for (int i = 0; i < task.DroidIntervals; i++)
                {
                    down = i * numberOfElements / task.DroidIntervals / task.Accuracy + task.DownBoundary;
                    up = (i + 1) * numberOfElements / task.DroidIntervals / task.Accuracy + task.DownBoundary;

                    if (i == 0)
                    {
                        down = task.DownBoundary;
                    }

                    if (i == task.DroidIntervals - 1)
                    {
                        up = task.UpBoundary;
                    }

                    var dist = _integrationDistributionFactory.GetInstance(task, down, up);

                    distributions.Add(dist);
                }
            });

            return distributions;
        }

        private async Task<IList<Integration>> ReadIntegrationTasksAsync()
        {
            IList<Integration> results = null;

            await Task.Run(() =>
            {
                results = _integrationsRepository.Read();
            });

            foreach(var item in results)
            {
                item.Distributions = null;
            }

            return results;
        }
    }
}
