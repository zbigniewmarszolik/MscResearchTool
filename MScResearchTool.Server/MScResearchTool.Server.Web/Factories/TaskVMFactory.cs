using System.Collections.Generic;
using MScResearchTool.Server.Core.Factories;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.ViewModels;
using MScResearchTool.Server.Core.Types;

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
