using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Microsoft.IdentityModel.Tokens;

namespace Aperea.Identity.Services
{
    public class WsFederationConfigurationBehavior : IServiceBehavior
    {
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            FederatedServiceCredentials.ConfigureServiceHost(serviceHostBase, new WsFederationConfiguration());
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}