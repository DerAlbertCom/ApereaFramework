using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Aperea.Infrastructure.IoC
{
    public class WebApiDependencyScope : IDependencyScope
    {
        private IContainer _container;

        public WebApiDependencyScope(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (_container == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            return _container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            if (_container != null)
                _container.Dispose();

            _container = null;
        }
    }
}