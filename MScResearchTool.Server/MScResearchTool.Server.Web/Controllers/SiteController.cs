using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Web.ViewModels;
using System.Diagnostics;
using System.Text;

namespace MScResearchTool.Server.Web.Controllers
{
    public class SiteController : Controller
    {
        [Route("robots.txt", Name = "GetRobotsText"), ResponseCache(Duration = 50000)]
        public ContentResult RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("disallow: /");

            return Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
