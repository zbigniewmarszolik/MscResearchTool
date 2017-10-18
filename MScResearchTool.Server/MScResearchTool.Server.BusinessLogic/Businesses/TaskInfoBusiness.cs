using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class TaskInfoBusiness : ITaskInfoBusiness
    {
        private IIntegrationTasksBusiness _integrationTasksBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }

        public TaskInfoBusiness
            (IIntegrationTasksBusiness integrationTasksBusiness,
            IIntegrationDistributionsBusiness integrationDistributionsBusiness)
        {
            _integrationTasksBusiness = integrationTasksBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
        }

        public async Task<TaskInfo> GetFullTasksAvailabilityAsync()
        {
            var model = new TaskInfo();

            await Task.Run(async () =>
            {
                var integrationTasks = await _integrationTasksBusiness.ReadAvailableFullIntegrationsAsync();
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
                var integrationDistributions = await _integrationDistributionsBusiness.ReadAvailableIntegrationDistributionsAsync();
                if (integrationDistributions.Count > 0)
                    model.IsIntegrationAvailable = true;
            });

            return model;
        }
    }
}
