using Autofac;
using MScResearchTool.Mobile.Android.Helpers;
using MScResearchTool.Mobile.Android.UI.Manual;
using MScResearchTool.Mobile.Android.UI.Manual.Contract;
using MScResearchTool.Mobile.Android.UI.Menu;
using MScResearchTool.Mobile.Android.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Android.AutofacModules
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
        }
    }
}