using Microsoft.AspNetCore.Mvc;

namespace MScResearchTool.Server.Web.Controllers
{
    public class CollectionController : Controller
    {
        public CollectionController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
