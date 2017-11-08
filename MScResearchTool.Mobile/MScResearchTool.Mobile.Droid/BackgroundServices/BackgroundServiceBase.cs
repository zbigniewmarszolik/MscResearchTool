using Android.App;
using Autofac;

namespace MScResearchTool.Mobile.Droid.BackgroundServices
{
    public abstract class BackgroundServiceBase : Service
    {
        protected IContainer Container => App.Container;
    }
}