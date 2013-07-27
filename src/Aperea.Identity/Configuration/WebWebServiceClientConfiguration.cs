using System;
using System.ServiceModel;

namespace Aperea.Identity.Configuration
{
    public class WebWebServiceClientConfiguration : IWebServiceClientConfiguration, IWebServiceClientConfigurator
    {
        string endpoint;

        public EndpointAddress GetEndpoint()
        {
            return new EndpointAddress(endpoint);
        }

        public IWebServiceClientConfigurator Endpoint(string endpointAddress)
        {
            if (endpointAddress == null)
                throw new ArgumentNullException("endpointAddress");
            endpoint = endpointAddress;
            return this;
        }
    }
}