using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using MScResearchTool.Windows.Domain.ViewModels;
using System;
using System.Windows.Input;

namespace MScResearchTool.Windows.WPF.ViewModels
{
    public class SelectionViewModel : BindableBase, ISelectionViewModel, ILoadableWindowViewModel
    {
        private ICommand _integrationCommand;
        public ICommand IntegrationCommand => _integrationCommand ?? (_integrationCommand = new DelegateCommand(() => OnIntegrationClicked()));

        public Action Clicked { get; set; }
        public Action WindowLoaded { get; set; }

        private void OnIntegrationClicked()
        {
            Clicked?.Invoke();
        }
    }
}
