using System;

namespace Aperea.Identity.Configuration
{
    public interface IWebServiceServerConfigurator
    {
        IWebServiceServerConfigurator SetServiceType(Type serviceType);
    }
}