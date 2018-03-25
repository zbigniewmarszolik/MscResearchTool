using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Factories
{
    public class CrackingDistributionFactory
    {
        public virtual CrackingDistribution GetInstance(Cracking task, int firstIndex, int lastIndex)
        {
            var instance = new CrackingDistribution()
            {
                ArchivePassword = task.ArchivePassword,
                ArchiveToCrack = task.ArchiveToCrack,
                AvailableCharacters = task.AvailableCharacters,
                CreationDate = task.CreationDate,
                FileName = task.FileName,
                Task = task,
                IsAvailable = true,
                IsFinished = false,
                RangeBeginning = CrackingCharacters.Instance().Characters[firstIndex],
                RangeEnding = CrackingCharacters.Instance().Characters[lastIndex],
                IsFounder = false,
                BatteryUsage = 0.0
            };

            return instance;
        }
    }
}
