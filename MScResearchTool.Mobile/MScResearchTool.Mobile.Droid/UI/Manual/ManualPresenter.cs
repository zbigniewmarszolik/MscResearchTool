using MScResearchTool.Mobile.Droid.Helpers;
using MScResearchTool.Mobile.Droid.UI.Manual.Contract;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Domain.Services;
using System.Threading.Tasks;
using System;
using MScResearchTool.Mobile.Domain.Models;
using Android.Content;
using Android.OS;

namespace MScResearchTool.Mobile.Droid.UI.Manual
{
    public class ManualPresenter : IManualPresenter
    {
        private IManualView _view { get; set; }
        private ITasksService _tasksService { get; set; }
        private ICrackingsService _crackingsService { get; set; }
        private ICrackingResultsService _crackingResultsService { get; set; }
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        private IIntegrationsService _integrationsService { get; set; }
        private IIntegrationResultsService _integrationResultsService { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private DroidHardwareHelper _droidHardwareHelper { get; set; }

        public ManualPresenter
            (ITasksService tasksService,
            ICrackingsService crackingsService,
            ICrackingResultsService crackingResultsService,
            ICrackingsBusiness crackingsBusiness,
            IIntegrationsService integrationsService,
            IIntegrationResultsService integrationResultsService,
            IIntegrationsBusiness integrationsBusiness,
            DroidHardwareHelper droidHardwareHelper)
        {
            _tasksService = tasksService;
            _crackingsService = crackingsService;
            _crackingResultsService = crackingResultsService;
            _crackingsBusiness = crackingsBusiness;
            _integrationsService = integrationsService;
            _integrationResultsService = integrationResultsService;
            _integrationsBusiness = integrationsBusiness;
            _droidHardwareHelper = droidHardwareHelper;
        }

        public async void OnTakeView(IManualView view)
        {
            _view = view;

            await CheckForTasks();
        }

        public async void RestartFlow()
        {
            await CheckForTasks();
        }

        public async void CrackButtonClicked()
        {
            _view.DisableAllButtons();
            _view.EnableProgressBar();

            CrackingDistribution cracking = null;

            try
            {
                cracking = await _crackingsService.GetCrackingAsync();
            }
            catch(Exception e)
            {
                _view.DisableProgressBar();
                _view.DisableAllButtons();
                _view.ShowServerError("Error connecting to the server for getting cracking task to break password with exception: " + "\n" + e.Message);
                return;
            }

            var batteryBeforeCracking = GetCurrentBatteryLevel();
            var result = await _crackingsBusiness.AttemptPasswordBreakingPasswordAsync(cracking);
            var batteryAfterCracking = GetCurrentBatteryLevel();

            if (result == null)
            {
                return;
            }

            result.CPU = _droidHardwareHelper.GetProcessorInfo();
            result.RAM = _droidHardwareHelper.GetMemoryAmount();
            result.BatteryUsage = batteryBeforeCracking - batteryAfterCracking;

            try
            {
                await _crackingResultsService.PostResultAsync(result);
            }
            catch(Exception e)
            {
                _view.DisableProgressBar();
                _view.ShowServerError("Error connecting to the server for posting cracking result with exception: " + "\n" + e.Message);
                return;
            }

            _view.DisableProgressBar();
            _view.ShowResult(result.PasswordResult, result.ElapsedSeconds);
        }

        public async void IntegrateButtonClicked()
        {
            _view.DisableAllButtons();
            _view.EnableProgressBar();

            IntegrationDistribution integration = null;

            try
            {
                integration = await _integrationsService.GetIntegrationAsync();
            }
            catch (Exception e)
            {
                _view.DisableProgressBar();
                _view.DisableAllButtons();
                _view.ShowServerError("Error connecting to the server for getting integration task to calculate with exception: " + "\n" + e.Message);
                return;
            }

            var batteryBeforeIntegrating = GetCurrentBatteryLevel();
            var result = await _integrationsBusiness.CalculateIntegrationAsync(integration);
            var batteryAfterIntegrating = GetCurrentBatteryLevel();

            result.CPU = _droidHardwareHelper.GetProcessorInfo();
            result.RAM = _droidHardwareHelper.GetMemoryAmount();
            result.BatteryUsage = batteryBeforeIntegrating - batteryAfterIntegrating;

            try
            {
                await _integrationResultsService.PostResultAsync(result);
            }
            catch (Exception e)
            {
                _view.DisableProgressBar();
                _view.ShowServerError("Error connecting to the server for posting integration result with exception: " + "\n" + e.Message);
                return;
            }

            _view.DisableProgressBar();
            _view.ShowResult(result.Result.ToString(), result.ElapsedSeconds);
        }

        public async void ReconnectButtonClicked()
        {
            await CheckForTasks();
        }

        public void OnDestroy()
        {
            _view = null;
        }

        private async Task CheckForTasks()
        {
            _view.DisableAllButtons();
            _view.EnableProgressBar();

            TaskInfo check = null;

            try
            {
                check = await _tasksService.GetTasksAvailabilityAsync();
            }
            catch (Exception e)
            {
                _view.DisableProgressBar();
                _view.ShowServerError("Error connecting to the server for reading available tasks.");
                return;
            }

            if (check.IsIntegrationAvailable)
                _view.EnableIntegration();

            _view.EnableReconnect();

            _view.DisableProgressBar();
        }

        private double GetCurrentBatteryLevel()
        {
            var batteryLevelString = BatteryManager.ExtraLevel;
            var batteryScaleString = BatteryManager.ExtraScale;

            var isLevelParseable = double.TryParse(batteryLevelString, out var batteryLevel);
            var isScaleParseable = double.TryParse(batteryScaleString, out var batteryScale);

            if(!isLevelParseable && !isScaleParseable)
            {
                return 0.0;
            }

            else return Math.Floor(batteryLevel * 100D / batteryScale);
        }
    }
}