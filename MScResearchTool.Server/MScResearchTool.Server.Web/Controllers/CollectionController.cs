using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Web.ViewModels;
using System.Collections.Generic;

namespace MScResearchTool.Server.Web.Controllers
{
    public class CollectionController : Controller
    {
        public CollectionController()
        {

        }

        public IActionResult Index()
        {
            return View(new List<TaskViewModel>());
        }
    }
}
