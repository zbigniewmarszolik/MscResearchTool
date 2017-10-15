using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Web.ViewModels;

namespace MScResearchTool.Server.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
