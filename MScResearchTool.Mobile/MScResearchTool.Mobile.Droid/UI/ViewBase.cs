using Android.App;
using Android.OS;
using Autofac;
using MScResearchTool.Mobile.Droid.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace MScResearchTool.Mobile.Droid.UI
{
    public abstract class ViewBase : Activity
    {
        private IContainer _container => App.Container;

        protected abstract void ViewComponentsInitialization();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var item in properties.Where(p => p.GetCustomAttributes(typeof(InjectDependencyAttribute), false).Any()))
            {
                object instance = null;

                if (!_container.TryResolve(item.PropertyType, out instance))
                {
                    throw new InvalidOperationException("Could not resolve type " + item.PropertyType.ToString());
                }

                item.SetValue(this, instance);
            }
        }
    }
}