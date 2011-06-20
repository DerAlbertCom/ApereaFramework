using System;
using Aperea.Infrastructure.Bootstrap;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Query;

namespace Aperea.Infrastructure.Registration
{
    public static class RegisterStructureMap
    {
        static IContainer _container;

        public static IContainer Container
        {
            get { return _container; }
        }

        public static void Execute()
        {
            _container = new Container();
            SetServiceLocator(_container);

            _container.Configure(c =>
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
                                       type => _container.Model.HasImplementationsFor(type)));

                c.For<IContainer>()
                    .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
                    .Use(_container);

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