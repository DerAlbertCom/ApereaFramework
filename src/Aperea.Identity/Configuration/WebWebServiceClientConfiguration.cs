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

        public IWebServiceClientConfigurator Endpoint(string endpointAddresse)
        {
            if (endpointAddresse == null)
                throw new ArgumentNullException("endpointAddresse");
            endpoint = endpointAddresse;
            return this;
        }
    }
}