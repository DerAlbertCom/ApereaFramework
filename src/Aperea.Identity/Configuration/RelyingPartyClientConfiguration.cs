using System;
using System.Collections.Concurrent;
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
            return new EndpointAddress(stsEndpoint);
        }

        EndpointAddress IRelyingPartyClientConfiguration.GetEndpointFor<T>()
        {
            return serviceConfigurations[typeof (T)].GetEndpoint();
        }

        readonly ConcurrentDictionary<Type, WebWebServiceClientConfiguration> serviceConfigurations =
            new ConcurrentDictionary<Type, WebWebServiceClientConfiguration>();

        string stsEndpoint;

        public IRelyingPartyClientConfigurator ConfigureService<T>(Action<IWebServiceClientConfigurator> serviceConfiguration)
        {
            var configuraiton = serviceConfigurations.GetOrAdd(typeof (T), c => new WebWebServiceClientConfiguration());
            serviceConfiguration(configuraiton);
            return this;
        }

        public IRelyingPartyClientConfigurator StsEndpoint(string endpointAddresse)
        {
            stsEndpoint = endpointAddresse;
            return this;
        }

        public static void Configure(Action<IRelyingPartyClientConfigurator> configuration)
        {
            configuration(CurrentConfiguration.Value);
        }
    }
}