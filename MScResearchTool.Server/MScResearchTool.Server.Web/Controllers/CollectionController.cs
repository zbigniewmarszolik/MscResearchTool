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
            var vm = new List<TaskViewModel>()
                {
                new TaskViewModel()
                {
                    IntegralSquares = new Core.Models.IntegralSquaresTask()
                    {
                        Id = 0,
                        IsActive = true,
                        AmountOfDroids = 2,
                        Result = null
                    },
                    IntegralTrapezoids = null,
                },
                new TaskViewModel()
                {
                    IntegralTrapezoids = new Core.Models.IntegralTrapezoidsTask()
                    {
                        Id = 7,
                        IsActive = true,
                        AmountOfDroids = 8,
                        Result = null
                    },
                    IntegralSquares = null
                }
            };

            foreach(var item in vm)
            {
                item.AssignTypeAndDroids();
            }

            return View(vm);
        }
    }
}
