using Autofac;
using MScResearchTool.Mobile.Android.Helpers;

namespace MScResearchTool.Mobile.Android.AutofacModules
{
    public class AndroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DroidHardwareHelper>().As<DroidHardwareHelper>();
        }
    }
}