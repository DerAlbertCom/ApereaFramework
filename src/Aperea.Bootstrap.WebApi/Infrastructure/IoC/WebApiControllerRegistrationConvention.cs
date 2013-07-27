using System;
using System.Web.Http.Controllers;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Aperea.Infrastructure.IoC
{
    public class WebApiControllerRegistrationConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (!type.IsAbstract && typeof (IHttpController).IsAssignableFrom(type))
            {
                registry.For(type).Use(type);
            }
        }
    }
}