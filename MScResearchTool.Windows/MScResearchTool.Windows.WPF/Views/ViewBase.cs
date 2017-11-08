using MScResearchTool.Windows.Domain.ViewModels;
using System.Windows.Controls;

namespace MScResearchTool.Windows.WPF.Views
{
    public abstract class ViewBase : UserControl
    {
        public ViewBase()
        {
            Loaded += ViewBase_Loaded;
        }

        private void ViewBase_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = DataContext as ILoadableWindowViewModel;

            vm?.WindowLoadedAction?.Invoke();
        }
    }
}
