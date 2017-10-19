using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Helpers;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Types;
using MScResearchTool.Server.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    public class IntegrationsController : Controller
    {
        private IIntegrationsBusiness _integrationTasksBusiness { get; set; }
        private IIntegrationDistributionsBusiness _integrationDistributionsBusiness { get; set; }
        private IIntegralInitializationHelper _integralInitializationHelper { get; set; }
        private IParseDoubleHelper _parseDoubleHelper { get; set; }

        public IntegrationsController
            (IIntegrationsBusiness integrationTasksBusiness,
            IIntegrationDistributionsBusiness integrationDistributionsBusiness,
            IIntegralInitializationHelper integralInitializationHelper,
            IParseDoubleHelper parseDoubleHelper)
        {
            _integrationTasksBusiness = integrationTasksBusiness;
            _integrationDistributionsBusiness = integrationDistributionsBusiness;
            _integralInitializationHelper = integralInitializationHelper;
            _parseDoubleHelper = parseDoubleHelper;
        }

        public IActionResult Index()
        {
            ViewData["InputError"] = null;

            var vm = new IntegrationViewModel()
            {
                Formula = "x*sin(x)",
                Precision = 1000,
                UpperLimit = "100",
                LowerLimit = "0",
                IntervalsCount = 2
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IntegrationViewModel integrationVm)
        {
            ViewData["InputError"] = null;

            if (integrationVm.IntervalsCount < 2)
            {
                ViewData["InputError"] = "Number of intervals must be at least 2 or greater.";
                return View(integrationVm);
            }

            var testFormula = _integralInitializationHelper.IsFormulaCorrectForCSharp(integrationVm.Formula);

            if (testFormula == false)
            {
                ViewData["InputError"] = "Wrong formula. It can not be evaluated. Please try again. Make sure you use 'x' as a variable.";
                return View(integrationVm);
            }

            var upperBoundParsed = _parseDoubleHelper.ParseInvariantCulture(integrationVm.UpperLimit);
            var lowerBoundParsed = _parseDoubleHelper.ParseInvariantCulture(integrationVm.LowerLimit);

            var testConstraints = _integralInitializationHelper.AreConstraintsCorrect(upperBoundParsed, lowerBoundParsed, integrationVm.Precision);

            if (testConstraints == false)
            {
                ViewData["InputError"] = "Wrong constraints. Make sure that 'b' > 'a' and 'n' is positive integer value.";
                return View(integrationVm);
            }

            var integral = new Integration()
            {
                CreationDate = DateTime.Now,
                DroidIntervals = integrationVm.IntervalsCount,
                UpBoundary = upperBoundParsed,
                DownBoundary = lowerBoundParsed,
                Accuracy = integrationVm.Precision,
                Formula = integrationVm.Formula,
                IsTrapezoidMethodRequested = _integralInitializationHelper.IsForTrapezoidIntegration(integrationVm.Method),
                IsFinished = false,
                IsTaken = false
            };

            await _integrationTasksBusiness.DistributeAndPersistAsync(integral);

            return RedirectToAction("Creation", "Tasks");
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
                    dto.IsTaken = true;
                    await _integrationTasksBusiness.UpdateAsync(dto);
                }  

                return Ok(dto);
            }

            else if (mode == ECalculationMode.Distributed.ToString())
            {
                var dto = await GetDistributedTask();

                if(dto != null)
                {
                    dto.IsTaken = true;
                    await _integrationDistributionsBusiness.UpdateAsync(dto);
                }

                return Ok(dto);
            }

            else return Ok(null);
        }

        private async Task<Integration> GetFullTask()
        {
            var result = await _integrationTasksBusiness.ReadAvailableAsync();

            return result.FirstOrDefault();
        }

        private async Task<IntegrationDistribution> GetDistributedTask()
        {
            var result = await _integrationDistributionsBusiness.ReadAvailableAsync();

            return result.FirstOrDefault();
        }
    }
}
