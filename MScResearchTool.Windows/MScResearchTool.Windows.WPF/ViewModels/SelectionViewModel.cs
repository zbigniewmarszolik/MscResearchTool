using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using MScResearchTool.Windows.Domain.Businesses;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Domain.ViewModels;
using System;
using System.Windows.Input;

namespace MScResearchTool.Windows.WPF.ViewModels
{
    public class SelectionViewModel : BindableBase, ISelectionViewModel, ILoadableWindowViewModel
    {
        public Action Clicked { get; set; }
        public Action WindowLoaded { get; set; }

        private ICommand _integrationCommand;
        public ICommand IntegrationCommand => _integrationCommand ?? (_integrationCommand = new DelegateCommand(() => OnIntegrationClicked()));

        private ITasksService _tasksService { get; set; }
        private IIntegrationsService _integrationsService { get; set; }
        private IIntegrationResultsService _integrationResultsService { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }

        private bool _isIntegrationEnabled;
        public bool IsIntegrationEnabled
        {
            get => _isIntegrationEnabled;
            set { _isIntegrationEnabled = value; OnPropertyChanged(() => IsIntegrationEnabled); }
        }

        private bool _isProgressing;
        public bool IsProgressing
        {
            get => _isProgressing;
            set { _isProgressing = value; OnPropertyChanged(() => IsProgressing); }
        }

        public SelectionViewModel
            (ITasksService tasksService, 
            IIntegrationsService integrationsService,
            IIntegrationResultsService integrationResultsService,
            IIntegrationsBusiness integrationsBusiness)
        {
            _tasksService = tasksService;
            _integrationsService = integrationsService;
            _integrationResultsService = integrationResultsService;
            _integrationsBusiness = integrationsBusiness;

            WindowLoaded = OnWindowLoaded;
        }

        public async void OnWindowLoaded()
        {
            IsIntegrationEnabled = false;
            IsProgressing = true;

            var available = await _tasksService.GetTasksAvailabilityAsync();

            if (available.IsIntegrationAvailable)
                IsIntegrationEnabled = true;

            IsProgressing = false;
        }

        private void OnIntegrationClicked()
        {
            ProcessIntegration();

            Clicked?.Invoke();
        }

        private async void ProcessIntegration()
        {
            var integrationTask = await _integrationsService.GetIntegrationAsync();

            var integrationResult = await _integrationsBusiness.CalculateIntegrationAsync(integrationTask);

            await _integrationResultsService.PostResultAsync(integrationResult);
        }
    }
}
