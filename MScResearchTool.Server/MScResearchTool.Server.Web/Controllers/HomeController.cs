using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Web.ViewModels;
using System.Collections.Generic;

namespace MScResearchTool.Server.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            var vm = new List<ResultViewModel>()
            {
                new ResultViewModel()
                {
                    IntegralSquares = new Core.Models.IntegralSquaresResult()
                    {
                        Id = 0,
                        Task = null,
                        AmountOfDroids = 3,
                        DroidTime = "2s",
                        WindowsTime = "1s"

                    },
                    IntegralTrapezoids = null
                },
                new ResultViewModel()
                {
                    IntegralSquares = new Core.Models.IntegralSquaresResult()
                    {
                        Id = 1,
                        Task = null,
                        AmountOfDroids = 3,
                        DroidTime = "4s",
                        WindowsTime = "2s"
                    },
                    IntegralTrapezoids = null
                },
                new ResultViewModel()
                {
                    IntegralSquares = null,
                    IntegralTrapezoids = new Core.Models.IntegralTrapezoidsResult()
                    {
                        Id = 2,
                        Task = null,
                        AmountOfDroids = 2,
                        DroidTime = "3s",
                        WindowsTime = "5s"
                    }
                }
            };

            foreach (var item in vm)
            {
                item.AssignTypeAndDroids();
            }

            return View(vm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
