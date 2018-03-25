using System;
using Microsoft.Practices.Prism.Mvvm;
using MScResearchTool.Windows.Domain.ViewModels;

namespace MScResearchTool.Windows.WPF.ViewModels
{
    public class StatusViewModel : BindableBase, IStatusViewModel, ILoadableWindowViewModel
    {
        public Action WindowLoadedAction { get; set; }

        private IMainViewModel MainVM { get; }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(() => StatusText); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(() => IsBusy); }
        }

        public StatusViewModel(IMainViewModel mainVM)
        {
            MainVM = mainVM;

            WindowLoadedAction = OnWindowLoaded;
        }

        public void OnWindowLoaded()
        {
            MainVM.SelectionViewModel.AppStateChangedAction = () =>
            {
                StatusText = AdjustText();
            };
        }

        private string AdjustText()
        {
            if (StatusText == "" || StatusText == null)
            {
                IsBusy = true;
                return "Obtaining task metadata.";
            }

            else if (StatusText == "Obtaining task metadata.")
                return "Processing task.";

            else if (StatusText == "Processing task.")
                return "Sending results to server.";

            else
            {
                IsBusy = false;
                return "";
            }
        }
    }
}
