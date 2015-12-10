using System;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;

namespace Aperea.Infrastructure.IoC
{
    internal class ControllerRegistryConvention : IRegistrationConvention
    {
        private void Process(Type type, IRegistry registry)
        {
            if (!type.IsAbstract && typeof (IController).IsAssignableFrom(type))
            {
                registry.AddType(type, type);
            }
        }

        public void ScanTypes(TypeSet types, Registry registry)
        {
            foreach (var type in types.AllTypes())
            {
                Process(type, registry);
            }
        }
    }
}