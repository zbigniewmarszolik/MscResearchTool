using Microsoft.Practices.Prism.Mvvm;
using MScResearchTool.Windows.Domain.ViewModels;

namespace MScResearchTool.Windows.WPF.ViewModels
{
    public class MainViewModel : BindableBase, IMainViewModel
    {
        private ISelectionViewModel _selectionViewModel;
        public ISelectionViewModel SelectionViewModel
        { 
            get => _selectionViewModel;
            set { _selectionViewModel = value; OnPropertyChanged(() => SelectionViewModel); }
        }

        private IStatusViewModel _statusViewModel;
        public IStatusViewModel StatusViewModel
        {
            get => _statusViewModel;
            set { _statusViewModel = value; OnPropertyChanged(() => StatusViewModel); }
        }
    }
}
