using MahApps.Metro.Controls;
using MScResearchTool.Windows.Domain.ViewModels;

namespace MScResearchTool.Windows.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IMainVM _vm { get; set; }

        public MainWindow(IMainVM vm)
        {
            _vm = vm;
            DataContext = _vm;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }
    }
}
