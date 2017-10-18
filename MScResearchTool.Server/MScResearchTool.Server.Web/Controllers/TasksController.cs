using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Factories;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Types;
using MScResearchTool.Server.Web.ViewModels;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    public class TasksController : Controller
    {
        private IIntegrationTasksBusiness _integrationsTasksBusiness { get; set; }
        private ITaskInfoBusiness _taskInfoBusiness { get; set; }
        private ITaskVMFactory<TaskViewModel> _taskVMFactory { get; set; }

        public TasksController
            (IIntegrationTasksBusiness integrationTasksBusiness,
            ITaskInfoBusiness taskInfoBusiness,
            ITaskVMFactory<TaskViewModel> taskVMFactory)
        {
            _integrationsTasksBusiness = integrationTasksBusiness;
            _taskInfoBusiness = taskInfoBusiness;
            _taskVMFactory = taskVMFactory;
        }

        public async Task<IActionResult> Index()
        {
            var integrations = await _integrationsTasksBusiness.ReadAllIntegrationTasksAsync();

            var vm = _taskVMFactory.GetCollection(integrations);

            return View(vm);
        }

        public IActionResult Creation()
        {
            return View();
        }

        public async Task<IActionResult> DeleteGenericTask(int deleteId, string taskType)
        {
            if (taskType.Contains("integration"))
                await _integrationsTasksBusiness.CascadeDeleteAsync(deleteId);

            return RedirectToAction("Index");
        }

        [Route("Api/CheckTasksAvailability/{mode}")]
        [HttpGet]
        public async Task<IActionResult> CheckTasksAvailability(string mode)
        {
            TaskInfo dto = null;

            if (mode == ECalculationMode.Single.ToString())
                dto = await _taskInfoBusiness.GetFullTasksAvailabilityAsync();

            else if(mode == ECalculationMode.Distributed.ToString())
                dto = await _taskInfoBusiness.GetDistributedTasksAvailabilityAsync();

            return Ok(dto);
        }
    }
}
