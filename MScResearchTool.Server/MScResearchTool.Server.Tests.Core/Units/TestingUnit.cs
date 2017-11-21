using System;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MScResearchTool.Server.Tests.Core.Units
{
    public class TestingUnit<T> where T : class
    {
        private Dictionary<Type, object> _dependencyCollection { get; } = new Dictionary<Type, object>();

        public void AddDependency<TDependency>(TDependency dependencyInjection)
        {
            _dependencyCollection.Add(typeof(TDependency), dependencyInjection);
        }

        public TDependency GetDependency<TDependency>() where TDependency : class
        {
            Type type = typeof(TDependency);

            object dependency;
            if (!_dependencyCollection.TryGetValue(type, out dependency))
            {
                throw new Exception($"Can not resolve type {type}. Object does not contain this kind of a dependency.");
            }

            return dependency as TDependency;
        }

        public T GetResolvedTestingUnit()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            foreach (var dependency in _dependencyCollection)
            {
                TypeInfo typeInfo = dependency.Key.GetTypeInfo();

                if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Mock<>))
                {
                    PropertyInfo propertyInfo = dependency.Key.GetProperty("Object",
                        BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    object value = propertyInfo.GetValue(dependency.Value);

                    serviceCollection.AddSingleton(dependency.Key.GenericTypeArguments[0], value);
                }
                else
                {
                    serviceCollection.AddSingleton(dependency.Key, dependency.Value);
                }
            }

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return ActivatorUtilities.CreateInstance<T>(serviceProvider);
        }
    }
}
