using MScResearchTool.Mobile.Android.Helpers;
using MScResearchTool.Mobile.Android.UI.Manual.Contract;
using MScResearchTool.Mobile.Domain.Businesses;
using MScResearchTool.Mobile.Domain.Services;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Android.UI.Manual
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

            var integration = await _integrationsService.GetIntegrationAsync();
            var result = await _integrationsBusiness.CalculateIntegrationAsync(integration);

            result.CPU = _droidHardwareHelper.GetProcessorInfo();
            result.RAM = _droidHardwareHelper.GetMemoryAmount();

            await _integrationResultsService.PostResultAsync(result);

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

            var check = await _tasksService.GetTasksAvailabilityAsync();

            if (check.IsIntegrationAvailable)
                _view.EnableIntegration();

            else _view.EnableReconnect();

            _view.DisableProgressBar();
        }
    }
}