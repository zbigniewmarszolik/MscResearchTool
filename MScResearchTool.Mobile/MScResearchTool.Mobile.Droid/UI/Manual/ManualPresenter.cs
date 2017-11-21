using MScResearchTool.Mobile.Droid.Helpers;
using MScResearchTool.Mobile.Droid.UI.Manual.Contract;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Domain.Services;
using System.Threading.Tasks;
using System;
using MScResearchTool.Mobile.Domain.Models;

namespace MScResearchTool.Mobile.Droid.UI.Manual
{
    public class ManualPresenter : IManualPresenter
    {
        private IManualView _view { get; set; }
        private ITasksService _tasksService { get; set; }
        private IIntegrationsService _integrationsService { get; set; }
        private IIntegrationResultsService _integrationResultsService { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private DroidHardwareHelper _droidHardwareHelper { get; set; }

        public ManualPresenter
            (ITasksService tasksService,
            IIntegrationsService integrationsService,
            IIntegrationResultsService integrationResultsService,
            IIntegrationsBusiness integrationsBusiness,
            DroidHardwareHelper droidHardwareHelper)
        {
            _tasksService = tasksService;
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
                _view.ShowServerError("Error connecting to the server for getting integration task to calculate.");
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
                _view.DisableProgressBar();
                _view.ShowServerError("Error connecting to the server for posting integration result.");
                return;
            }

            _view.DisableProgressBar();
            _view.ShowResult(result.Result, result.ElapsedSeconds);
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
    }
}