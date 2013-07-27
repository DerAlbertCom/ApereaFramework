using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using Aperea.Identity.Configuration;

namespace Aperea.Identity.Services
{
    public class WsFederationServiceHostFactory : ServiceHostFactory
    {
        readonly IRelyingPartyServerConfiguration _configuration;

        public WsFederationServiceHostFactory()
        {
            _configuration = RelyingPartyServerConfiguration.Current;
        }

        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var serviceType = Type.GetType(constructorString);
            var host = new ServiceHost(serviceType, OnlyHttpsAddresses(baseAddresses));
            ConfigureServiceHost(host);
            return host;
        }

        static Uri[] OnlyHttpsAddresses(IEnumerable<Uri> baseAddresses)
        {
            return baseAddresses.Where(uri=>uri.Scheme=="https").ToArray();
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new ServiceHost(serviceType, OnlyHttpsAddresses(baseAddresses));
            ConfigureServiceHost(host);
            return host;
        }

        void ConfigureServiceHost(ServiceHost host)
        {
//        host.AddDefaultEndpoints();
            AddDefaultEndpoints(host);
            BindEndpointsToWs2007Federation(host);
            AddBehaviors(host);
        }

        void AddDefaultEndpoints(ServiceHost host)
        {
            foreach (var baseAddress in host.BaseAddresses)
            {
                host.AddServiceEndpoint(GetServiceContractType(host.Description.ServiceType), CreateWS2007FederationHttpBinding(), baseAddress);
            }
        }

        Type GetServiceContractType(Type serviceType)
        {
            if (serviceType == null)
                return null;
            if (serviceType.GetCustomAttributes(typeof(ServiceContractAttribute),false).Any())
            {
                return serviceType;
            }
            foreach (var type in serviceType.GetInterfaces())
            {
                var contractType = GetServiceContractType(type);
                if (contractType!=null)
                {
                    return contractType;
                }
            }
            return null;
        }

        void AddBehaviors(ServiceHost host)
        {
            var useRequestHeader = FindOrAdd<UseRequestHeadersForMetadataAddressBehavior>(host);

            useRequestHeader.DefaultPortsByScheme.Clear();

            var query = from u in _configuration.AudienceUris
                        select new
                                   {
                                       u.Scheme,
                                       u.Port
                                   };
            foreach (var uri in query.Distinct())
            {
                useRequestHeader.DefaultPortsByScheme.Add(uri.Scheme, uri.Port);
            }

            var metaData = FindOrAdd<ServiceMetadataBehavior>(host);

            metaData.HttpsGetEnabled = true;

            FindOrAdd<WsFederationConfigurationBehavior>(host);
            FindOrAdd<ServiceLocatorBehavior>(host);
        }

        static T FindOrAdd<T>(ServiceHostBase host) where T: class, IServiceBehavior
        {
            var behavior = host.Description.Behaviors.Find<T>();
            if (behavior == null)
            {
                behavior = Activator.CreateInstance<T>();
                host.Description.Behaviors.Add(behavior);
            }
            return behavior;
        }

        void BindEndpointsToWs2007Federation(ServiceHost host)
        {
            foreach (var endpoint in host.Description.Endpoints)
            {
                endpoint.Binding = CreateWS2007FederationHttpBinding();
            }
        }

        WS2007FederationHttpBinding CreateWS2007FederationHttpBinding()
        {
            var binding = new WS2007FederationHttpBinding(WSFederationHttpSecurityMode.TransportWithMessageCredential);
            binding.Security.Message.EstablishSecurityContext = false;
            binding.Security.Message.IssuerMetadataAddress =
                new EndpointAddress(_configuration.WebServiceIssuer.Uri.OriginalString + "/mex");
            binding.Security.Message.IssuerAddress = new EndpointAddress(_configuration.WebServiceIssuer.Uri);
            return binding;
        }
    }
}