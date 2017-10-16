using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Types;
using MScResearchTool.Server.Web.ViewModels;
using System;
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
                    CreationDate = DateTime.Now,
                    ModelId = 5,
                    DroidsCount = 3,
                    TaskType = ETaskType.Trapezoid_integration.ToString()
                },
                new TaskViewModel()
                {
                    CreationDate = new DateTime(1993,11,29,23,51,35),
                    ModelId = 3,
                    DroidsCount = 5,
                    TaskType = ETaskType.Square_integration.ToString()
                }
            };

            foreach(var item in vm)
            {
                item.FixTaskType();
            }

            return View(vm);
        }

        public IActionResult DeleteTask(int deleteId)
        {
            return Ok();
        }
    }
}
