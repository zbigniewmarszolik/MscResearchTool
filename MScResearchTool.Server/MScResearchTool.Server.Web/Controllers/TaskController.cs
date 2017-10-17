using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.Helpers;
using MScResearchTool.Server.Web.ViewModels;

namespace MScResearchTool.Server.Web.Controllers
{
    public class TaskController : Controller
    {
        private IntegralFormulaHelper _integralFormulaHelper { get; set; }

        public TaskController(IntegralFormulaHelper integralFormulaHelper)
        {
            _integralFormulaHelper = integralFormulaHelper;
        }

        public IActionResult Index()
        {
            ViewData["FormulaError"] = null;
            return View();
        }

        public IActionResult CreateIntegration()
        {
            ViewData["FormulaError"] = null;

            var vm = new IntegralViewModel()
            {
                Formula = "x*sin(x)"
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateIntegration(IntegralViewModel integralVm)
        {
            ViewData["FormulaError"] = null;

            var testFormula = _integralFormulaHelper.IsFormulaCorrectForCSharp(integralVm.Formula);

            if (testFormula == false)
            {
                ViewData["FormulaError"] = "Wrong formula. It can not be evaluated. Please try again. Make sure you use 'x' as a variable.";
                return View(integralVm);
            }

            var integral = new IntegrationTask();

            var testMethod = _integralFormulaHelper.IsForTrapezoidIntegration(integralVm.Method);

            if (testMethod == true)
                integral.IsTrapezoidMethodRequested = true;

            else integral.IsTrapezoidMethodRequested = false;

            //PERSISTENCE

            return RedirectToAction("Index");
        }
    }
}
