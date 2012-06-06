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
            Container = new Container();
            SetServiceLocator(Container);
            Container.Configure(c =>
                {
                    c.Scan(x =>
                        {
                            x.AssembliesFromApplicationBaseDirectory(StructureMapAssemblyFilter.Filter);
                            x.TheCallingAssembly();
                            x.AddAllTypesOf<IBootstrapItem>();
                            x.WithDefaultConventions();
                            x.LookForRegistries();
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

        static void SetServiceLocator(IContainer container)
        {
            var locator = new StructureMapServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}