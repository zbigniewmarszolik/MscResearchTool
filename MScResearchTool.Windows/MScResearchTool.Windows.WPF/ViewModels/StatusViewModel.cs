using System;
using Microsoft.Practices.Prism.Mvvm;
using MScResearchTool.Windows.Domain.ViewModels;

namespace MScResearchTool.Windows.WPF.ViewModels
{
    public class StatusViewModel : BindableBase, IStatusViewModel, ILoadableWindowViewModel
    {
        public Action WindowLoaded { get; set; }

        private IMainViewModel MainVM { get; }

        private string _statusText;
        public string StatusText
        {

            get => _statusText;

            set
            {
                _statusText = value;
                OnPropertyChanged(() => StatusText);
            }
        }

        public StatusViewModel(IMainViewModel mainVM)
        {
            MainVM = mainVM;

            WindowLoaded = OnWindowLoaded;
        }

        public void OnWindowLoaded()
        {
            MainVM.SelectionViewModel.Clicked = () =>
            {
                StatusText = "TESTING...";
            };
        }
    }
}
