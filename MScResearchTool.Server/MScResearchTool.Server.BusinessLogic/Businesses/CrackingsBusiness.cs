using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MScResearchTool.Server.BusinessLogic.Factories;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class CrackingsBusiness : ICrackingsBusiness
    {
        private ICrackingsRepository _crackingsRepository { get; set; }
        private CrackingDistributionFactory _crackingDistributionFactory { get; set; }

        public CrackingsBusiness
            (ICrackingsRepository crackingsRepository,
            CrackingDistributionFactory crackingDistributionFactory)
        {
            _crackingsRepository = crackingsRepository;
            _crackingDistributionFactory = crackingDistributionFactory;
        }

        public async Task DistributeAndPersistAsync(Cracking crackingTask)
        {
            await Task.Run(async () =>
            {
                crackingTask.Distributions = await DistributeCrackingsAsync(crackingTask);

                _crackingsRepository.Create(crackingTask);
            });
        }

        public async Task<IList<Cracking>> ReadAllAsync()
        {
            return await ReadCrackingTasksAsync();
        }

        public async Task<IList<Cracking>> ReadAllEagerAsync()
        {
            IList<Cracking> results = null;

            await Task.Run(() =>
            {
                results = _crackingsRepository.ReadEager();
            });

            return results;
        }

        public async Task<IList<Cracking>> ReadAvailableAsync()
        {
            var resultSet = await ReadCrackingTasksAsync();

            return resultSet.Where(x => x.IsAvailable && !x.IsFinished).ToList();
        }

        public async Task<Cracking> ReadByIdAsync(int taskId)
        {
            var singleResult = await ReadCrackingTasksAsync();

            return singleResult.Where(x => x.Id == taskId).First();
        }

        public async Task UpdateAsync(Cracking integrationTask)
        {
            await Task.Run(() =>
            {
                _crackingsRepository.Update(integrationTask);
            });
        }

        public async Task UnstuckTakenAsync()
        {
            var set = await ReadCrackingTasksAsync();
            var stuck = set.Where(x => x.IsFinished == false && x.IsAvailable == false).ToList();

            await Task.Run(() =>
            {
                foreach (var item in stuck)
                {
                    item.IsAvailable = true;
                    _crackingsRepository.Update(item);
                }
            });
        }

        public async Task UnstuckByIdAsync(int taskId)
        {
            var task = await ReadByIdAsync(taskId);

            await Task.Run(() =>
            {
                if (!task.IsAvailable && !task.IsFinished)
                {
                    task.IsAvailable = true;
                    _crackingsRepository.Update(task);
                }
            });
        }

        public async Task CascadeDeleteAsync(int taskId)
        {
            await Task.Run(() =>
            {
                _crackingsRepository.Delete(taskId);
            });
        }

        private async Task<IList<CrackingDistribution>> DistributeCrackingsAsync(Cracking task)
        {
            IList<CrackingDistribution> distributions = new List<CrackingDistribution>();

            var charactersCollectionCount = CrackingCharacters.Instance().Characters.Count;

            var elementsPerDistribution = charactersCollectionCount / task.DroidRanges;

            await Task.Run(() =>
            {
                for(int i = 0; i < task.DroidRanges; i++)
                {
                    var startingElementIndex = i * elementsPerDistribution;
                    var endingElementIndex = i * elementsPerDistribution + elementsPerDistribution - 1;

                    if(endingElementIndex >= charactersCollectionCount)
                    {
                        endingElementIndex = charactersCollectionCount - 1;
                    }

                    var dist = _crackingDistributionFactory.GetInstance(task, startingElementIndex, endingElementIndex);

                    distributions.Add(dist);
                }
            });

            return distributions;
        }

        private async Task<IList<Cracking>> ReadCrackingTasksAsync()
        {
            IList<Cracking> results = null;

            await Task.Run(() =>
            {
                results = _crackingsRepository.Read();
            });

            foreach (var item in results)
            {
                item.Distributions = null;
            }

            return results;
        }
    }
}
