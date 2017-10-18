using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Helpers;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    public class TaskController : Controller
    {
        private IIntegrationTasksBusiness _integrationTasksBusiness { get; set; }
        private IIntegralInitializationHelper _integralInitializationHelper { get; set; }
        private IParseDoubleHelper _parseDoubleHelper { get; set; }

        public TaskController
            (IIntegrationTasksBusiness integrationTasksBusiness,
            IIntegralInitializationHelper integralInitializationHelper,
            IParseDoubleHelper parseDoubleHelper)
        {
            _integrationTasksBusiness = integrationTasksBusiness;
            _integralInitializationHelper = integralInitializationHelper;
            _parseDoubleHelper = parseDoubleHelper;
        }

        public IActionResult Index()
        {
            ViewData["InputError"] = null;
            return View();
        }

        public IActionResult CreateIntegration()
        {
            ViewData["InputError"] = null;

            var vm = new IntegrationViewModel()
            {
                Formula = "x*sin(x)",
                Precision = 1000,
                UpperLimit = "100",
                LowerLimit = "0"
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIntegration(IntegrationViewModel integrationVm)
        {
            ViewData["InputError"] = null;

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

            var integral = new IntegrationTask()
            {
                CreationDate = DateTime.Now,
                DroidIntervals = integrationVm.IntervalsCount,
                UpBoundary = upperBoundParsed,
                DownBoundary = lowerBoundParsed,
                Accuracy = integrationVm.Precision,
                Formula = integrationVm.Formula,
                IsTrapezoidMethodRequested = _integralInitializationHelper.IsForTrapezoidIntegration(integrationVm.Method)
            };

            await _integrationTasksBusiness.DistributeAndPersist(integral);

            return RedirectToAction("Index");
        }
    }
}
