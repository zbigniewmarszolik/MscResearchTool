using System.Collections.Generic;
using MScResearchTool.Server.Core.Factories;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.ViewModels;
using MScResearchTool.Server.Core.Types;
using System.Linq;

namespace MScResearchTool.Server.Web.Factories
{
    public class TaskVMFactory : ITaskVMFactory<TaskViewModel>
    {
        public IList<TaskViewModel> GetCollection(IList<IntegrationTask> integrations)
        {
            var taskViewModels = new List<TaskViewModel>();

            foreach (var item in integrations)
            {
                var viewModelComponent = new TaskViewModel()
                {
                    CreationDate = item.CreationDate,
                    DroidsCount = item.DroidIntervals,
                    ModelId = item.Id
                };

                if (item.IsTrapezoidMethodRequested)
                    viewModelComponent.TaskType = ETaskType.Trapezoid_integration.ToString();

                else viewModelComponent.TaskType = ETaskType.Square_integration.ToString();

                if (!item.IsFinished && item.IsTaken)
                    viewModelComponent.TaskStatus = "Main task stuck. ";

                else if (item.IsFinished)
                    viewModelComponent.TaskStatus = "Main task finished. ";

                else viewModelComponent.TaskStatus = "Main task waiting. ";

                if (item.Distributions.Any(x => x.IsTaken && !x.IsFinished))
                    viewModelComponent.TaskStatus += "One of more distributions stuck.";

                else if (item.Distributions.All(x => x.IsFinished))
                    viewModelComponent.TaskStatus += "All distributions finished.";

                else viewModelComponent.TaskStatus += "Distributions waiting.";

                taskViewModels.Add(viewModelComponent);
            }

            foreach (var item in taskViewModels)
            {
                item.FixTaskType();
            }

            return taskViewModels;
        }
    }
}
