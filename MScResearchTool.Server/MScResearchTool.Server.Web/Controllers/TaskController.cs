using Microsoft.AspNetCore.Mvc;

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
    }
}
