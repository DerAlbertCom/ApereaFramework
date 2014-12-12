using System;
using Aperea.Infrastructure.Bootstrap;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Graph;
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
            Container = new Container();
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
                    .Use(typeof (Lazy<>));

                c.For<IServiceLocator>().Use(context=>new StructureMapServiceLocator(context.GetInstance<IContainer>()));
            });
        }

        private static void SetServiceLocator(IContainer container)
        {
            var locator = new StructureMapServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}