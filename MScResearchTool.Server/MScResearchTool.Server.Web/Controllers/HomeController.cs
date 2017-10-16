using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Web.ViewModels;
using System.Collections.Generic;
using System;

namespace MScResearchTool.Server.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            var vm = new List<ReportViewModel>()
            {
                new ReportViewModel()
                {
                    Id = 0,
                    GenerationDate = new DateTime(2016,10,03,05,00,15),
                    Title = "Integration by trapezoids method",
                    ContentPdf = null
                },
                new ReportViewModel()
                {
                    Id = 1,
                    GenerationDate = new DateTime(2014,12,14,17,32,51),
                    Title = "Integration by squares method",
                    ContentPdf = null
                },
                new ReportViewModel()
                {
                    Id = 2,
                    GenerationDate = DateTime.Now,
                    Title = "Integration by trapezoids method",
                    ContentPdf = null
                },
            };

            return View(vm);
        }

        public IActionResult DeleteReport(int removeId)
        {
            return Ok();
        }

        public IActionResult DownloadReport(int downloadId)
        {
            return Ok();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
