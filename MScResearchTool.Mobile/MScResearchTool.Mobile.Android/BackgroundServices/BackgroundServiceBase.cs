using Android.App;
using Autofac;

namespace MScResearchTool.Mobile.Android.BackgroundServices
{
    abstract class BackgroundServiceBase : Service
    {
        protected IContainer Container => App.Container;
    }
}