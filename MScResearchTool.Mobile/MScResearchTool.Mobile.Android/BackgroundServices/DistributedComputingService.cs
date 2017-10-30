﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using MScResearchTool.Mobile.Domain.Services;
using System.Threading.Tasks;
using System;
using System.Threading;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Android.Helpers;
using Autofac;

namespace MScResearchTool.Mobile.Android.BackgroundServices
{
    [Service]
    public class DistributedComputingService : BackgroundServiceBase
    {
        private ITasksService _tasksService { get; set; }
        private IIntegrationsService _integrationsService { get; set; }
        private IIntegrationResultsService _integrationResultsService { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private DroidHardwareHelper _droidHardwareHelper { get; set; }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            ConstructServiceComponents();

            Task.Run(async () =>
            {
                await ControlComputations();
            });

            return StartCommandResult.Sticky;
        }

        private async Task ControlComputations()
        {
            bool shouldTakeBreak = false;

            while (true)
            {
                if (shouldTakeBreak)
                    Thread.Sleep(60000);

                var check = await _tasksService.GetTasksAvailabilityAsync();

                if (check.IsIntegrationAvailable)
                {
                    shouldTakeBreak = false;

                    var integration = await _integrationsService.GetIntegrationAsync();
                    var result = await _integrationsBusiness.CalculateIntegrationAsync(integration);

                    result.CPU = _droidHardwareHelper.GetProcessorInfo();
                    result.RAM = _droidHardwareHelper.GetMemoryAmount();

                    await _integrationResultsService.PostResultAsync(result);
                }

                else shouldTakeBreak = true;
            }
        }

        private void ConstructServiceComponents()
        {
            _tasksService = Container.Resolve<ITasksService>();
            _integrationsService = Container.Resolve<IIntegrationsService>();
            _integrationResultsService = Container.Resolve<IIntegrationResultsService>();
            _integrationsBusiness = Container.Resolve<IIntegrationsBusiness>();
            _droidHardwareHelper = Container.Resolve<DroidHardwareHelper>();
        }
    }
}