using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Identity.Services
{
    public class ServiceLocatorInstanceProvider : IInstanceProvider
    {
        readonly IServiceLocator _locator;
        readonly Type _serviceType;

        public ServiceLocatorInstanceProvider(Type serviceType)
        {
            _locator = ServiceLocator.Current;
            _serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext,  null);
        }

        public object GetInstance(InstanceContext instanceContext,   Message message)
        {
            return _locator.GetInstance(_serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}