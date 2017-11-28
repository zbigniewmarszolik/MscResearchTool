using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.Converters;
using MScResearchTool.Server.Web.Facades;
using MScResearchTool.Server.Web.Factories;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }
        private ITaskInfoBusiness _taskInfoBusiness { get; set; }
        private DeleteStrategyFactory _deleteStrategyFactory { get; set; }
        private UnstuckStrategyFactory _unstuckStrategyFactory { get; set; }
        private TaskVMFacade _taskVMFacade { get; set; }
        private TaskTypeConverter _taskTypeConverter { get; set; }

        public TasksController
            (IIntegrationsBusiness integrationsBusiness,
            IIntegrationDistributionsBusiness integrationDistributionsBusiness,
            ITaskInfoBusiness taskInfoBusiness,
            DeleteStrategyFactory deleteStrategyFactory,
            UnstuckStrategyFactory unstuckStrategyFactory,
            TaskVMFacade taskVMFacade,
            TaskTypeConverter taskTypeConverter)
        {
            _integrationsBusiness = integrationsBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
            _taskInfoBusiness = taskInfoBusiness;
            _deleteStrategyFactory = deleteStrategyFactory;
            _unstuckStrategyFactory = unstuckStrategyFactory;
            _taskVMFacade = taskVMFacade;
            _taskTypeConverter = taskTypeConverter;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _taskVMFacade.GetTaskViewModels();

            return View(vm);
        }

        public IActionResult Creation()
        {
            return View();
        }

        public async Task<IActionResult> DeleteGenericTask(int deleteId, string taskType)
        {
            await _deleteStrategyFactory.ResolveDeleteStrategy(_taskTypeConverter.StringToEnumerator(taskType)).DeleteAsync(deleteId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnstuckGenericTask(int unstuckId, string taskType)
        {
            await _unstuckStrategyFactory.ResolveUnstuckStrategy(_taskTypeConverter.StringToEnumerator(taskType)).UnstuckAsync(unstuckId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnstuckAllTasks()
        {
            await _integrationDistributionsBusiness.UnstuckTakenAsync();
            await _integrationsBusiness.UnstuckTakenAsync();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
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
