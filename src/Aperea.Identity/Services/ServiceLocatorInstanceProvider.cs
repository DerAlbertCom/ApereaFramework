using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Identity.Services
{
    public class ServiceLocatorInstanceProvider : IInstanceProvider
    {
        readonly IServiceLocator locator;
        readonly Type serviceType;

        public ServiceLocatorInstanceProvider(Type serviceType)
        {
            locator = ServiceLocator.Current;
            this.serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return locator.GetInstance(serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}