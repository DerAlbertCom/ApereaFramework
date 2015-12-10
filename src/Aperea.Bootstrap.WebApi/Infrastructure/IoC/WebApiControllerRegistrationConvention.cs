using System;
using System.Web.Http.Controllers;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;

namespace Aperea.Infrastructure.IoC
{
    public class WebApiControllerRegistrationConvention : IRegistrationConvention
    {
        private void Process(Type type, IProfileRegistry registry)
        {
            if (!type.IsAbstract && typeof (IHttpController).IsAssignableFrom(type))
            {
                registry.For(type).Use(type);
            }
        }

        public void ScanTypes(TypeSet types, Registry registry)
        {
            foreach (var type in types.AllTypes())
            {
                Process(type,registry);
            }
        }
    }
}