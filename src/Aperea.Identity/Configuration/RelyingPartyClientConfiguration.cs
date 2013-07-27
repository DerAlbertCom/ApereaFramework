using System;
using System.Collections.Concurrent;
using System.IdentityModel.Protocols.WSTrust;
using System.ServiceModel;

namespace Aperea.Identity.Configuration
{
    public class RelyingPartyClientConfiguration : IRelyingPartyClientConfiguration, IRelyingPartyClientConfigurator
    {
        RelyingPartyClientConfiguration()
        {
        }

        static readonly Lazy<RelyingPartyClientConfiguration> CurrentConfiguration =
            new Lazy<RelyingPartyClientConfiguration>(CreateDefaultConfiguration);

        static RelyingPartyClientConfiguration CreateDefaultConfiguration()
        {
            return new RelyingPartyClientConfiguration();
        }

        public static IRelyingPartyClientConfiguration Current
        {
            get { return CurrentConfiguration.Value; }
        }

        EndpointAddress IRelyingPartyClientConfiguration.GetStsEndpoint()
        {
            return new EndpointAddress(_stsEndpoint);
        }

        public EndpointAddress GetEndpointFor<T>()
        {
            return _serviceConfigurations[typeof(T)].GetEndpoint();
        }

        public EndpointReference GetEndpointReferenceFor<T>()
        {
            return new EndpointReference(GetEndpointFor<T>().Uri.ToString());
        }

        readonly ConcurrentDictionary<Type, WebWebServiceClientConfiguration> _serviceConfigurations =
            new ConcurrentDictionary<Type, WebWebServiceClientConfiguration>();

        string _stsEndpoint;

        public IRelyingPartyClientConfigurator ConfigureService<T>(Action<IWebServiceClientConfigurator> serviceConfiguration)
        {
            var configuration = _serviceConfigurations.GetOrAdd(typeof (T), c => new WebWebServiceClientConfiguration());
            serviceConfiguration(configuration);
            return this;
        }

        public IRelyingPartyClientConfigurator StsEndpoint(string endpointAddress)
        {
            _stsEndpoint = endpointAddress;
            return this;
        }

        public static void Configure(Action<IRelyingPartyClientConfigurator> configuration)
        {
            configuration(CurrentConfiguration.Value);
        }
    }
}