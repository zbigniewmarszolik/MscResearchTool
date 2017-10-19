using System.Windows;

namespace MScResearchTool.Windows.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var setup = new Setup();
        }
    }
}
