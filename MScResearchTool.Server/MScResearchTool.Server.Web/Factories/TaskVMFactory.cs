using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.ViewModels;
using MScResearchTool.Server.Core.Types;
using System.Linq;

namespace MScResearchTool.Server.Web.Factories
{
    public class TaskVMFactory
    {
        public IList<TaskViewModel> GetCollection(IList<Integration> integrations)
        {
            var collection = new List<TaskViewModel>();

            foreach (var item in integrations)
            {
                var collectionElement = new TaskViewModel()
                {
                    CreationDate = item.CreationDate,
                    DroidsCount = item.DroidIntervals,
                    ModelId = item.Id
                };

                if (item.IsTrapezoidMethodRequested)
                    collectionElement.TaskType = ETaskType.Trapezoid_integration.ToString();

                else collectionElement.TaskType = ETaskType.Square_integration.ToString();

                if (!item.IsFinished && !item.IsAvailable)
                    collectionElement.TaskStatus = "Main task in progress (or stuck). ";

                else if (item.IsFinished)
                    collectionElement.TaskStatus = "Main task finished. ";

                else collectionElement.TaskStatus = "Main task waiting. ";

                if (item.Distributions.All(x => x.IsFinished))
                {
                    collectionElement.TaskStatus += "All distributions finished.";
                }

                else if(item.Distributions.All(x => x.IsAvailable))
                {
                    collectionElement.TaskStatus += "Distributions waiting.";
                }

                else collectionElement.TaskStatus += "One of more distributions in progress (or stuck).";

                collection.Add(collectionElement);
            }

            foreach (var item in collection)
            {
                item.FixTaskType();
            }

            return collection;
        }
    }
}
