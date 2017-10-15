using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Web.ViewModels;

namespace MScResearchTool.Server.Web.Controllers
{
    public class TaskController : Controller
    {
        public TaskController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateIntegration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateIntegration(IntegralViewModel integralVm)
        {
            return RedirectToAction("Index");
        }
    }
}
