using Android.App;
using Autofac;

namespace MScResearchTool.Mobile.Droid.UI
{
    public abstract class ViewBase : Activity
    {
        protected IContainer Container => App.Container;

        protected abstract void ViewComponentsInitialization();
    }
}