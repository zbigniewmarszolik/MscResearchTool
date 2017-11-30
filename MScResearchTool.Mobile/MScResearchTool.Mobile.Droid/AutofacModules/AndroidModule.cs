using Autofac;
using MScResearchTool.Mobile.Droid.Converters;
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
            builder.RegisterType<ButtonValuesConverter>();

            builder.RegisterType<DroidHardwareHelper>();
            builder.RegisterType<ProcessHelper>();

            builder.RegisterType<MenuPresenter>().As<IMenuPresenter>();
            builder.RegisterType<ManualPresenter>().As<IManualPresenter>();
        }
    }
}