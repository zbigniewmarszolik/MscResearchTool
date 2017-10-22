using MScResearchTool.Windows.Domain.ViewModels;
using MScResearchTool.Windows.WPF.Windows;

namespace MScResearchTool.Windows.WPF
{
    public class ViewManager
    {
        private IMainViewModel ViewModel { get; }
        
        public ViewManager
            (IMainViewModel viewModel,
            ISelectionViewModel selectionViewModel,
            IStatusViewModel statusViewModel)
        {
            ViewModel = viewModel;

            ViewModel.SelectionViewModel = selectionViewModel;
            ViewModel.StatusViewModel = statusViewModel;
        }

        public void Open()
        {
            var mainWindow = new MainWindow
            {
                DataContext = ViewModel
            };

            mainWindow.Show();
        }
    }
}
