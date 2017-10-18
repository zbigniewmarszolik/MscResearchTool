using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Factories;
using MScResearchTool.Server.Web.ViewModels;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    public class TasksController : Controller
    {
        private IIntegrationTasksBusiness _integrationsTasksBusiness { get; set; }
        private ITaskVMFactory<TaskViewModel> _taskVMFactory { get; set; }

        public TasksController
            (IIntegrationTasksBusiness integrationTasksBusiness,
            ITaskVMFactory<TaskViewModel> taskVMFactory)
        {
            _integrationsTasksBusiness = integrationTasksBusiness;
            _taskVMFactory = taskVMFactory;
        }

        public async Task<IActionResult> Index()
        {
            var integrations = await _integrationsTasksBusiness.ReadAllIntegrationTasks();

            var vm = _taskVMFactory.GetCollection(integrations);

            return View(vm);
        }

        public async Task<IActionResult> DeleteGenericTask(int deleteId, string taskType)
        {
            if (taskType.Contains("integration"))
                await _integrationsTasksBusiness.CascadeDelete(deleteId);

            return RedirectToAction("Index");
        }

        public IActionResult Creation()
        {
            return View();
        }
    }
}
