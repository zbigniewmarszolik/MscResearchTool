using Autofac;
using MScResearchTool.Mobile.Droid.Helpers;
using MScResearchTool.Mobile.Droid.UI.Manual;
using MScResearchTool.Mobile.Droid.UI.Manual.Contract;
using MScResearchTool.Mobile.Droid.UI.Menu;
using MScResearchTool.Mobile.Droid.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Droid.AutofacModules
{
    public class AndroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MenuPresenter>().As<IMenuPresenter>();
            builder.RegisterType<MenuActivity>().As<IMenuView>();
            builder.RegisterType<ManualPresenter>().As<IManualPresenter>();
            builder.RegisterType<ManualActivity>().As<IManualView>();
            builder.RegisterType<DroidHardwareHelper>().As<DroidHardwareHelper>();
            builder.RegisterType<ProcessHelper>().As<ProcessHelper>();
        }
    }
}