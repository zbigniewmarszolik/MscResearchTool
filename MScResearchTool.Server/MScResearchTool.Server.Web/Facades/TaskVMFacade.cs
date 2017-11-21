using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.Factories;
using MScResearchTool.Server.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Facades
{
    public class TaskVMFacade
    {
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private TaskVMFactory _taskVMFactory { get; set; }

        public TaskVMFacade(IIntegrationsBusiness integrationsBusiness, TaskVMFactory taskVMFactory)
        {
            _integrationsBusiness = integrationsBusiness;
            _taskVMFactory = taskVMFactory;
        }

        public async Task<IList<TaskViewModel>> GetTaskViewModels()
        {
            IList<TaskViewModel> viewModels = new List<TaskViewModel>();

            var integrations = await ReadIntegrationsAsync();

            var integrationTasks = _taskVMFactory.GetIntegrationsCollection(integrations);

            foreach(var item in integrationTasks)
            {
                viewModels.Add(item);
            }

            return viewModels;
        }

        private async Task<IList<Integration>> ReadIntegrationsAsync()
        {
            return await _integrationsBusiness.ReadAllEagerAsync();
        }
    }
}
