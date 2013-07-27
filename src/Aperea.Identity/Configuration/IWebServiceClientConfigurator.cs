using System;

namespace Aperea.Identity.Configuration
{
    public interface IWebServiceClientConfigurator
    {
        IWebServiceClientConfigurator Endpoint(string endpointAddress);
    }
}