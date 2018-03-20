using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class TaskInfoBusiness : ITaskInfoBusiness
    {
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        private ICrackingDistributionsBusiness _crackingDistributionsBusiness { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }

        public TaskInfoBusiness
            (ICrackingsBusiness crackingsBusiness,
            ICrackingDistributionsBusiness crackingDistributionsBusiness,
            IIntegrationsBusiness integrationsBusiness,
            IIntegrationDistributionsBusiness integrationDistributionsBusiness)
        {
            _crackingsBusiness = crackingsBusiness;
            _crackingDistributionsBusiness = crackingDistributionsBusiness;
            _integrationsBusiness = integrationsBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
        }

        public async Task<TaskInfo> GetFullTasksAvailabilityAsync()
        {
            var model = new TaskInfo();

            await Task.Run(async () =>
            {
                var crackingTasks = await _crackingsBusiness.ReadAvailableAsync();
                if (crackingTasks.Count > 0)
                    model.IsCrackingAvailable = true;

                var integrationTasks = await _integrationsBusiness.ReadAvailableAsync();
                if (integrationTasks.Count > 0)
                    model.IsIntegrationAvailable = true;
            });

            return model;
        }

        public async Task<TaskInfo> GetDistributedTasksAvailabilityAsync()
        {
            var model = new TaskInfo();

            await Task.Run(async () =>
            {
                var crackingDistributions = await _crackingDistributionsBusiness.ReadAvailableAsync();
                if (crackingDistributions.Count > 0)
                    model.IsCrackingAvailable = true;

                var integrationDistributions = await _integrationDistributionsBusiness.ReadAvailableAsync();
                if (integrationDistributions.Count > 0)
                    model.IsIntegrationAvailable = true;
            });

            return model;
        }
    }
}
