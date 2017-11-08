﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using MScResearchTool.Windows.Domain.Businesses;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Domain.ViewModels;
using MScResearchTool.Windows.WPF.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace MScResearchTool.Windows.WPF.ViewModels
{
    public class SelectionViewModel : BindableBase, ISelectionViewModel, ILoadableWindowViewModel
    {
        public Action AppStateChangedAction { get; set; }
        public Action WindowLoadedAction { get; set; }

        private ICommand _integrationCommand;
        public ICommand IntegrationCommand => _integrationCommand ?? (_integrationCommand = new DelegateCommand(() => OnIntegrationClicked()));

        private ICommand _reconnectCommand;
        public ICommand ReconnectCommand => _reconnectCommand ?? (_reconnectCommand = new DelegateCommand(() => OnReconnectClicked()));

        private ITasksService _tasksService { get; set; }
        private IIntegrationsService _integrationsService { get; set; }
        private IIntegrationResultsService _integrationResultsService { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }

        private bool _isReconnectEnabled;
        public bool IsReconnectEnabled
        {
            get => _isReconnectEnabled;
            set { _isReconnectEnabled = value; OnPropertyChanged(() => IsReconnectEnabled); }
        }

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

        private Visibility _textBoxVisibility;
        public Visibility TextBoxVisibility
        {
            get => _textBoxVisibility;
            set { _textBoxVisibility = value; OnPropertyChanged(() => TextBoxVisibility); }
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

            _tasksService.ConnectionErrorAction = x => PopServerError(x);
            _integrationsService.ConnectionErrorAction = x => PopServerError(x);
            _integrationResultsService.ConnectionErrorAction = x => PopServerError(x);

            WindowLoadedAction = OnWindowLoaded;
        }

        private void PopServerError(string errorContent)
        {
            IsProgressing = false;
            TextBoxVisibility = Visibility.Hidden;

            var messageBox = new MessageWindow("Connection error", errorContent, "OK");
            messageBox.ShowDialog();
        }

        public async void OnWindowLoaded()
        {
            TextBoxVisibility = Visibility.Visible;
            IsReconnectEnabled = false;
            IsIntegrationEnabled = false;
            IsProgressing = true;

            var available = await _tasksService.GetTasksAvailabilityAsync();

            IsProgressing = false;
            TextBoxVisibility = Visibility.Hidden;

            if (available == null)
            {
                IsReconnectEnabled = true;
                return;
            }
            
            else if (available.IsIntegrationAvailable)
                IsIntegrationEnabled = true;

            IsReconnectEnabled = true;
        }

        private void OnReconnectClicked()
        {
            OnWindowLoaded();
        }

        private void OnIntegrationClicked()
        {
            ProcessIntegration();

            AppStateChangedAction?.Invoke();
        }

        private async void ProcessIntegration()
        {
            IsReconnectEnabled = false;
            IsIntegrationEnabled = false;

            var integrationTask = await _integrationsService.GetIntegrationAsync();

            AppStateChangedAction?.Invoke();

            if (integrationTask == null)
                return;

            var integrationResult = await _integrationsBusiness.CalculateIntegrationAsync(integrationTask);

            AppStateChangedAction?.Invoke();

            await _integrationResultsService.PostResultAsync(integrationResult);

            AppStateChangedAction?.Invoke();

            var messageBox = new MessageWindow("Task finished", 
                "Task calculated with following result: " + integrationResult.Result.ToString() + ". Elapsed time: " + integrationResult.ElapsedSeconds.ToString() + " seconds. ", "OK");
            messageBox.ShowDialog();

            OnWindowLoaded();
        }
    }
}
