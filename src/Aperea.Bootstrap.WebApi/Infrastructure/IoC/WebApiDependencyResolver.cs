using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using StructureMap;

namespace Aperea.Infrastructure.IoC
{
    public class WebApiDependencyResolver : WebApiDependencyScope, IDependencyResolver, IHttpControllerActivator
    {
        private readonly IContainer _container;
        
        public WebApiDependencyResolver(IContainer container)
            : base(container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;

            _container.Inject(typeof(IHttpControllerActivator), this);
        }

        public IDependencyScope BeginScope()
        {
            return new WebApiDependencyScope(_container.GetNestedContainer());
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return _container.GetNestedContainer().GetInstance(controllerType) as IHttpController;
        }
    }
}