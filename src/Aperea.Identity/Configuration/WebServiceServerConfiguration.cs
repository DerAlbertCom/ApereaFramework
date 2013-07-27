using System;

namespace Aperea.Identity.Configuration
{
    public class WebServiceServerConfiguration : IWebServiceServerConfiguration, IWebServiceServerConfigurator
    {
        public WebServiceServerConfiguration(Type type)
        {
            ServiceType = type;
        }

        public Type ServiceType { get; private set; }


        public IWebServiceServerConfigurator SetServiceType(Type serviceType)
        {
            ServiceType = serviceType;
            return this;
        }

    }
}