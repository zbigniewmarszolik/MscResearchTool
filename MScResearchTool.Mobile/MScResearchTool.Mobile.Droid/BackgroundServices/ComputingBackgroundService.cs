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
using Android.Util;

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
        private ICrackingsService _crackingsService { get; set; }
        [InjectDependency]
        private ICrackingResultsService _crackingResultsService { get; set; }
        [InjectDependency]
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        [InjectDependency]
        private IIntegrationsService _integrationsService { get; set; }
        [InjectDependency]
        private IIntegrationResultsService _integrationResultsService { get; set; }
        [InjectDependency]
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        [InjectDependency]
        private DroidHardwareHelper _droidHardwareHelper { get; set; }

        private Handler _handler { get; set; }

        private static string TAG = typeof(ComputingBackgroundService).Name;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Info(TAG, "OnStartCommand()");
            base.OnStartCommand(intent, flags, startId);

            _handler = new Handler(Looper.MainLooper);

            var t = new Java.Lang.Thread(() =>
            {
                Task.Run(async () =>
                {
                    await ControlComputations();
                });
            });

            t.Start();

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            Log.Info(TAG, "OnDestroy()");
            SelfKill();
        }

        public override void OnTaskRemoved(Intent rootIntent)
        {
            Log.Info(TAG, "OnTaskRemoved()");
            base.OnTaskRemoved(rootIntent);
        }

        private async Task ControlComputations()
        {
            TaskInfo taskInfo = null;

            bool shouldTakeBreak = false;

            while (true)
            {
                Log.Info(TAG, "operation_start");
                CheckBattery();

                if (shouldTakeBreak)
                {
                    Log.Info(TAG, "BREAK");
                    Thread.Sleep(180000);
                }

                else Log.Info(TAG, "CONTINUE");

                try
                {
                    taskInfo = await _tasksService.GetTasksAvailabilityAsync();
                }
                catch (Exception e)
                {
                    BackgroundError("Service failed in connecting to the server for reading available tasks.");
                    shouldTakeBreak = true;
                    taskInfo = null;
                    continue;
                }

                if(taskInfo.IsCrackingAvailable)
                {
                    shouldTakeBreak = false;

                    await TryCrack();
                }

                else if (taskInfo.IsIntegrationAvailable)
                {
                    shouldTakeBreak = false;

                    await Integrate();
                }

                else shouldTakeBreak = true;
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

        private void CheckBattery()
        {
            var intentFilter = new IntentFilter(Intent.ActionBatteryChanged);
            var batteryRegister = RegisterReceiver(null, intentFilter);
            int batteryLevel = batteryRegister.GetIntExtra(BatteryManager.ExtraLevel, -1);
            int batteryScale = batteryRegister.GetIntExtra(BatteryManager.ExtraScale, -1);

            int batteryPercentage = (int)Math.Floor(batteryLevel * 100D / batteryScale);

            Log.Info(TAG, "CheckBattery()");

            if (batteryPercentage < BatteryBorder)
            {
                Log.Info(TAG, "KILL_BECAUSE_BATTERY");
                SelfKill();
            }   
        }

        private void SelfKill()
        {
            StopSelf();
            Process.KillProcess(Process.MyPid());
        }

        private async Task TryCrack()
        {
            CrackingDistribution cracking = null;

            try
            {
                cracking = await _crackingsService.GetCrackingAsync();
            }
            catch(Exception e)
            {
                BackgroundError("Service failed in connecting to the server for getting integration task to break password with following exception: " + "\n" + e.Message);
                return;
            }

            var result = await _crackingsBusiness.AttemptPasswordBreakingPasswordAsync(cracking);

            if (result == null)
            {
                return;
            }

            result.CPU = _droidHardwareHelper.GetProcessorInfo();
            result.RAM = _droidHardwareHelper.GetMemoryAmount();

            try
            {
                await _crackingResultsService.PostResultAsync(result);
            }
            catch (Exception e)
            {
                BackgroundError("Service failed in connecting to the server for posting cracking result with following exception: " + "\n" + e.Message);
                return;
            }
        }

        private async Task Integrate()
        {
            IntegrationDistribution integration = null;

            try
            {
                integration = await _integrationsService.GetIntegrationAsync();
            }
            catch (Exception e)
            {
                BackgroundError("Service failed in connecting to the server for getting integration task to calculate with following exception: " + "\n" + e.Message);
                return;
            }

            var result = await _integrationsBusiness.CalculateIntegrationAsync(integration);

            result.CPU = _droidHardwareHelper.GetProcessorInfo();
            result.RAM = _droidHardwareHelper.GetMemoryAmount();

            try
            {
                await _integrationResultsService.PostResultAsync(result);
            }
            catch (Exception e)
            {
                BackgroundError("Service failed in connecting to the server for posting integration result with following exception: " + "\n" + e.Message);
                return;
            }
        }
    }
}