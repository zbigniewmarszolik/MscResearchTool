using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.Builders;
using MScResearchTool.Server.Web.Factories;
using MScResearchTool.Server.Web.Helpers;
using MScResearchTool.Server.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    public class IntegrationsController : Controller
    {
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }
        private IIntegrationResultsBusiness _integrationResultsBusiness { get; set; }
        private IntegrationVMFactory _integrationVMFactory { get; set; }
        private IntegralInitializationHelper _integralInitializationHelper { get; set; }
        private ParseDoubleHelper _parseDoubleHelper { get; set; }

        public IntegrationsController
            (IIntegrationsBusiness integrationsBusiness,
            IIntegrationDistributionsBusiness integrationDistributionsBusiness,
            IIntegrationResultsBusiness integrationResultsBusiness,
            IntegrationVMFactory integrationVMFactory,
            IntegralInitializationHelper integralInitializationHelper,
            ParseDoubleHelper parseDoubleHelper)
        {
            _integrationsBusiness = integrationsBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
            _integrationResultsBusiness = integrationResultsBusiness;
            _integrationVMFactory = integrationVMFactory;
            _integralInitializationHelper = integralInitializationHelper;
            _parseDoubleHelper = parseDoubleHelper;
        }

        public IActionResult Index()
        {
            ViewData["InputError"] = null;

            var vm = _integrationVMFactory.GetInstance();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IntegrationViewModel integrationVm, string submitter)
        {
            ViewData["InputError"] = null;

            var readyFormula = "";

            if (integrationVm.IntervalsCount < 2)
            {
                ViewData["InputError"] = "Number of intervals must be at least 2 or greater.";
                return View(integrationVm);
            }

            var isFormulaEvaluateable = _integralInitializationHelper.IsFormulaCorrectForCSharp(integrationVm.Formula);

            if (isFormulaEvaluateable == false)
            {
                ViewData["InputError"] = "Wrong formula. It can not be evaluated. Please try again. Make sure you use 'x' as a variable.";
                return View(integrationVm);
            }

            else readyFormula = _integralInitializationHelper.PrepareFormulaForExpression(integrationVm.Formula);


            var upperBoundParsed = _parseDoubleHelper.ParseInvariantCulture(integrationVm.UpperLimit);
            var lowerBoundParsed = _parseDoubleHelper.ParseInvariantCulture(integrationVm.LowerLimit);

            var areConstraintsCorrect = _integralInitializationHelper.AreConstraintsCorrect(upperBoundParsed, lowerBoundParsed, integrationVm.Precision);

            if (areConstraintsCorrect == false)
            {
                ViewData["InputError"] = "Wrong constraints. Make sure that 'b' > 'a' and 'n' is positive integer value.";
                return View(integrationVm);
            }

            var builder = new IntegrationBuilder();
            builder.NumberOfDistributions = integrationVm.IntervalsCount;
            builder.UpperLimit = upperBoundParsed;
            builder.LowerLimit = lowerBoundParsed;
            builder.IntegrationPrecision = integrationVm.Precision;
            builder.IntegrationFormula = readyFormula;
            builder.UnmodifiedInputFormula = integrationVm.Formula;
            builder.IsTrapezoidMethodRequired = _integralInitializationHelper.IsForTrapezoidIntegration(integrationVm.Method);

            var integral = builder.Build();

            await _integrationsBusiness.DistributeAndPersistAsync(integral);

            switch(submitter)
            {
                case "Add single integration task":
                    return RedirectToAction("Creation", "Tasks");

                case "Add integration task and prepare next":
                    return View(integrationVm);

                default: return RedirectToAction("Creation", "Tasks");
            }
        }

        [Route("Api/PostIntegrationResult/{mode}")]
        [HttpPut]
        public async Task<IActionResult> PostIntegrationResult([FromBody]IntegrationResult integrated)
        {
            await _integrationResultsBusiness.ProcessResultAsync(integrated);

            return Ok();
        }

        [Route("Api/GetIntegration/{mode}")]
        [HttpGet]
        public async Task<IActionResult> GetIntegration(string mode)
        {
            if (mode == ECalculationMode.Single.ToString())
            {
                var dto = await GetFullTask();

                if (dto != null)
                {
                    dto.IsAvailable = false;
                    await _integrationsBusiness.UpdateAsync(dto);
                }

                return Ok(dto);
            }

            else if (mode == ECalculationMode.Distributed.ToString())
            {
                var dto = await GetDistributedTask();

                if(dto != null)
                {
                    dto.IsAvailable = false;
                    await _integrationDistributionsBusiness.UpdateAsync(dto);
                }

                dto.Task = null;

                return Ok(dto);
            }

            else return Ok(null);
        }

        private async Task<Integration> GetFullTask()
        {
            var result = await _integrationsBusiness.ReadAvailableAsync();

            return result.FirstOrDefault();
        }

        private async Task<IntegrationDistribution> GetDistributedTask()
        {
            var result = await _integrationDistributionsBusiness.ReadAvailableAsync();

            return result.FirstOrDefault();
        }
    }
}
