using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.ViewModels;
using MScResearchTool.Server.Web.Converters;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Web.Strategies.StatusStrategy;

namespace MScResearchTool.Server.Web.Factories
{
    public class TaskVMFactory
    {
        private TaskTypeConverter _taskTypeConverter { get; set; }
        private StatusStrategyFactory _statusStrategyFactory { get; set; }

        public TaskVMFactory(TaskTypeConverter taskTypeConverter, StatusStrategyFactory statusStrategyFactory)
        {
            _taskTypeConverter = taskTypeConverter;
            _statusStrategyFactory = statusStrategyFactory;
        }

        public IList<TaskViewModel> GetCrackingsCollection(IList<Cracking> crackings)
        {
            var collection = new List<TaskViewModel>();

            foreach(var item in crackings)
            {
                var collectionElement = new TaskViewModel()
                {
                    CreationDate = item.CreationDate,
                    DroidsCount = item.DroidRanges,
                    ModelId = item.Id,
                    TaskType = _taskTypeConverter.EnumeratorToString(ETaskType.Cracking)
                };

                var state = new Status(item.IsFinished, item.IsAvailable);

                IList<Status> distStates = new List<Status>();

                foreach(var j in item.Distributions)
                {
                    distStates.Add(new Status(j.IsFinished, j.IsAvailable));
                }

                var mainStatusStrategy = _statusStrategyFactory.CreateForMainTask(state);
                collectionElement.TaskStatus = mainStatusStrategy.MainTaskStatus();

                var distributionsStatusStrategy = _statusStrategyFactory.CreateForDistributions(distStates);
                collectionElement.TaskStatus += distributionsStatusStrategy.DistributionsStatus();

                collection.Add(collectionElement);
            }

            return collection;
        }

        public IList<TaskViewModel> GetIntegrationsCollection(IList<Integration> integrations)
        {
            var collection = new List<TaskViewModel>();

            foreach (var item in integrations)
            {
                var collectionElement = new TaskViewModel()
                {
                    CreationDate = item.CreationDate,
                    DroidsCount = item.DroidIntervals,
                    ModelId = item.Id,
                    TaskType = "unavailable"
                };

                if (item.IsTrapezoidMethodRequested)
                    collectionElement.TaskType = _taskTypeConverter.EnumeratorToString(ETaskType.TrapezoidIntegration);

                else collectionElement.TaskType = _taskTypeConverter.EnumeratorToString(ETaskType.SquareIntegration);

                var state = new Status(item.IsFinished, item.IsAvailable);

                IList<Status> distStates = new List<Status>();

                foreach(var j in item.Distributions)
                {
                    distStates.Add(new Status(j.IsFinished, j.IsAvailable));
                }

                var mainStatusStrategy = _statusStrategyFactory.CreateForMainTask(state);
                collectionElement.TaskStatus = mainStatusStrategy.MainTaskStatus();

                var distributionsStatusStrategy = _statusStrategyFactory.CreateForDistributions(distStates);
                collectionElement.TaskStatus += distributionsStatusStrategy.DistributionsStatus();

                collection.Add(collectionElement);
            }

            return collection;
        }
    }
}
