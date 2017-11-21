using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using MScResearchTool.Mobile.Domain.Services;
using System.Threading.Tasks;
using System.Threading;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Droid.Helpers;
using Android.Widget;
using MScResearchTool.Mobile.Domain.Models;
using System;
using MScResearchTool.Mobile.Droid.Attributes;

namespace MScResearchTool.Mobile.Droid.BackgroundServices
{
    [Service(Name = "msc.research.tool",
        Process = "msc.research.tool.bgservice_process",
        Exported = true)]
    public class ComputingBackgroundService : BackgroundServiceBase
    {
        [InjectDependency]
        private ITasksService _tasksService { get; set; }
        [InjectDependency]
        private IIntegrationsService _integrationsService { get; set; }
        [InjectDependency]
        private IIntegrationResultsService _integrationResultsService { get; set; }
        [InjectDependency]
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        [InjectDependency]
        private DroidHardwareHelper _droidHardwareHelper { get; set; }

        private Handler _handler { get; set; }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            base.OnStartCommand(intent, flags, startId);

            _handler = new Handler(Looper.MainLooper);

            Task.Run(async () =>
            {
                await ControlComputations();
            });

            return StartCommandResult.NotSticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private async Task ControlComputations()
        {
            TaskInfo taskInfo = null;

            bool shouldTakeBreak = false;

            while (true)
            {
                if (shouldTakeBreak)
                    Thread.Sleep(180000);

                try
                {
                    taskInfo = await _tasksService.GetTasksAvailabilityAsync();
                }
                catch(Exception e)
                {
                    BackgroundError("Service failed in connecting to the server for reading available tasks.");
                    shouldTakeBreak = true;
                    taskInfo = null;
                    continue;
                }

                if (taskInfo.IsIntegrationAvailable)
                {
                    shouldTakeBreak = false;

                    await Integrate();
                }

                else shouldTakeBreak = true;
            }
        }

        private async Task Integrate()
        {
            IntegrationDistribution integration = null;

            try
            {
                integration = await _integrationsService.GetIntegrationAsync();
            }
            catch(Exception e)
            {
                BackgroundError("Service failed in connecting to the server for getting integration task to calculate.");
                return;
            }

            var result = await _integrationsBusiness.CalculateIntegrationAsync(integration);

            result.CPU = _droidHardwareHelper.GetProcessorInfo();
            result.RAM = _droidHardwareHelper.GetMemoryAmount();

            try
            {
                await _integrationResultsService.PostResultAsync(result);
            }
            catch(Exception e)
            {
                BackgroundError("Service failed in connecting to the server for posting integration result.");
                return;
            }
        }

        private void BackgroundError(string errorValue)
        {
            Java.Lang.Runnable runnableToast = new Java.Lang.Runnable(() =>
            {
                var duration = ToastLength.Long;
                Toast.MakeText(this, errorValue, duration).Show();
            });

            _handler.Post(runnableToast);
        }
    }
}