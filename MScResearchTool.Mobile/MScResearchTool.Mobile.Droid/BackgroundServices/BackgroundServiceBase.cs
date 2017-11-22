using Android.App;
using Android.Content;
using Android.Runtime;
using Autofac;
using MScResearchTool.Mobile.Droid.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace MScResearchTool.Mobile.Droid.BackgroundServices
{
    public abstract class BackgroundServiceBase : Service
    {
        private IContainer _container => App.Container;

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
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

            return StartCommandResult.NotSticky;
        }
    }
}