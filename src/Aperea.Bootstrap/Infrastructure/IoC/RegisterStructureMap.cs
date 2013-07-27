using System;
using Aperea.Infrastructure.Bootstrap;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Pipeline;

namespace Aperea.Infrastructure.IoC
{
    public static class RegisterStructureMap
    {
        public static IContainer Container { get; private set; }

        public static void Execute()
        {
            if (Container != null)
                return;
            Container = ObjectFactory.Container;
            SetServiceLocator(Container);
            Container.Configure(c =>
            {
                c.Scan(s =>
                {
                    s.AssembliesForApplication();
                    s.TheCallingAssembly();
                    s.AddAllTypesOf<IBootstrapItem>();
                    s.WithDefaultConventions();
                    s.LookForRegistries();
                });

                c.For(typeof (Lazy<>))
                    .Use(typeof (Lazy<>))
                    .WithProperty("isThreadSafe").EqualTo(true);

                c.SetAllProperties(x =>
                    x.TypeMatches(
                        type => Container.Model.HasImplementationsFor(type)));

                c.For<IServiceLocator>()
                    .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
                    .Use(ServiceLocator.Current);
            });
        }

        private static void SetServiceLocator(IContainer container)
        {
            var locator = new StructureMapServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}