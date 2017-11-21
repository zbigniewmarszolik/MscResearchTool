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
        private IContainer Container => App.Container;

        protected abstract void ViewComponentsInitialization();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            PropertyInfo[] properties =
                this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var property in properties.Where(p => p.GetCustomAttributes(typeof(InjectDependencyAttribute), false).Any()))
            {
                object instance = null;
                if (!App.Container.TryResolve(property.PropertyType, out instance))
                {
                    throw new InvalidOperationException("Could not resolve type " + property.PropertyType.ToString());
                }

                property.SetValue(this, instance);
            }
        }
    }
}