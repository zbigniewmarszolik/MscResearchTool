using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Types;
using MScResearchTool.Server.Web.Factories;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    public class TasksController : Controller
    {
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }
        private ITaskInfoBusiness _taskInfoBusiness { get; set; }
        private TaskVMFactory _taskVMFactory { get; set; }

        public TasksController
            (IIntegrationsBusiness integrationsBusiness,
            IIntegrationDistributionsBusiness integrationDistributionsBusiness,
            ITaskInfoBusiness taskInfoBusiness,
            TaskVMFactory taskVMFactory)
        {
            _integrationsBusiness = integrationsBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
            _taskInfoBusiness = taskInfoBusiness;
            _taskVMFactory = taskVMFactory;
        }

        public async Task<IActionResult> Index()
        {
            var integrations = await _integrationsBusiness.ReadAllEagerAsync();

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
                await _integrationsBusiness.CascadeDeleteAsync(deleteId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnstuckGenericTask(int unstuckId, string taskType)
        {
            if (taskType.Contains("integration"))
            {
                await _integrationDistributionsBusiness.UnstuckByIdAsync(unstuckId);
                await _integrationsBusiness.UnstuckByIdAsync(unstuckId);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnstuckAllTasks()
        {
            await _integrationDistributionsBusiness.UnstuckTakenAsync();
            await _integrationsBusiness.UnstuckTakenAsync();

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
