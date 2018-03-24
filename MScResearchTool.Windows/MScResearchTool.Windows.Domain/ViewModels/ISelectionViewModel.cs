using System;
using System.Windows;
using System.Windows.Input;

namespace MScResearchTool.Windows.Domain.ViewModels
{
    public interface ISelectionViewModel
    {
        Action AppStateChangedAction { get; set; }

        ICommand CrackingCommand { get; }
        ICommand IntegrationCommand { get; }
        ICommand ReconnectCommand { get; }

        Visibility TextBoxVisibility { get; set; }

        bool IsReconnectEnabled { get; set; }
        bool IsIntegrationEnabled { get; set; }
        bool IsProgressing { get; set; }
    }
}