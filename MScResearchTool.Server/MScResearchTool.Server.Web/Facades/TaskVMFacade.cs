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
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private TaskVMFactory _taskVMFactory { get; set; }

        public TaskVMFacade
            (ICrackingsBusiness crackingsBusiness,
            IIntegrationsBusiness integrationsBusiness,
            TaskVMFactory taskVMFactory)
        {
            _crackingsBusiness = crackingsBusiness;
            _integrationsBusiness = integrationsBusiness;
            _taskVMFactory = taskVMFactory;
        }

        public async Task<IList<TaskViewModel>> GetTaskViewModels()
        {
            IList<TaskViewModel> viewModels = new List<TaskViewModel>();

            var crackings = await ReadCrackingsAsync();
            var crackingTasks = _taskVMFactory.GetCrackingsCollection(crackings);

            foreach(var item in crackingTasks)
            {
                viewModels.Add(item);
            }

            var integrations = await ReadIntegrationsAsync();
            var integrationTasks = _taskVMFactory.GetIntegrationsCollection(integrations);

            foreach (var item in integrationTasks)
            {
                viewModels.Add(item);
            }

            return viewModels;
        }

        private async Task<IList<Cracking>> ReadCrackingsAsync()
        {
            return await _crackingsBusiness.ReadAllEagerAsync();
        }

        private async Task<IList<Integration>> ReadIntegrationsAsync()
        {
            return await _integrationsBusiness.ReadAllEagerAsync();
        }
    }
}
