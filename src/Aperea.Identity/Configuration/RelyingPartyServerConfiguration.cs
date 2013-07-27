using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Activation;
using System.Web.Routing;
using Aperea.Identity.Services;

namespace Aperea.Identity.Configuration
{
    public class RelyingPartyServerConfiguration : IRelyingPartyServerConfiguration, IRelyingPartyServerConfigurator
    {
        static readonly Lazy<RelyingPartyServerConfiguration> CurrentConfiguration =
            new Lazy<RelyingPartyServerConfiguration>(CreateDefaultConfiguration);

        static RelyingPartyServerConfiguration CreateDefaultConfiguration()
        {
            return new RelyingPartyServerConfiguration();
        }

        public static IRelyingPartyServerConfiguration Current
        {
            get { return CurrentConfiguration.Value; }
        }

        RelyingPartyServerConfiguration()
        {
            _serviceHostFactory = new WsFederationServiceHostFactory();
        }

        readonly List<Uri> _audienceUris = new List<Uri>();

        public IRelyingPartyServerConfigurator SetBaseUri(string url)
        {
            if (!url.EndsWith("/"))
                url += "/";
            BaseUrl = url;
            AudienceUri(url);
            return this;
        }

        public IRelyingPartyServerConfigurator AudienceUri(string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            _audienceUris.Add(new Uri(url));
            return this;
        }

        public string BaseUrl { get; private set; }

        public IEnumerable<Uri> AudienceUris
        {
            get { return _audienceUris; }
        }

        ServiceHostFactoryBase _serviceHostFactory;


        readonly ConcurrentDictionary<Type, WebServiceServerConfiguration> _configurations =
            new ConcurrentDictionary<Type, WebServiceServerConfiguration>();

        public static void Configure(Action<IRelyingPartyServerConfigurator> configuration)
        {
            configuration(CurrentConfiguration.Value);
        }

        public IRelyingPartyServerConfigurator ConfigureService<T>(string routePrefix,
                                                          Action<IWebServiceServerConfigurator> configurator)
        {
            var configuration = _configurations.GetOrAdd(typeof(T), t => new WebServiceServerConfiguration(t));
            if (configurator != null)
                configurator(configuration);

            AudienceUri(BaseUrl + routePrefix);

            RouteTable.Routes.Add(new ServiceRoute(
                                      routePrefix: routePrefix,
                                      serviceHostFactory: _serviceHostFactory,
                                      serviceType: configuration.ServiceType)
                );
            return this;
        }

        public IRelyingPartyServerConfigurator ConfigureService<T>(string routePrefix)
        {
            return ConfigureService<T>(routePrefix, null);
        }

        public IRelyingPartyServerConfigurator ServiceHostFactory(
            WsFederationServiceHostFactory wsFederationServiceHostFactory)
        {
            _serviceHostFactory = wsFederationServiceHostFactory;
            return this;
        }


        readonly List<TrustedIssuer> _trustedIssuers = new List<TrustedIssuer>();

        public IRelyingPartyServerConfigurator TrustedIssuer(string thumbprint, string name)
        {
            _trustedIssuers.Add(new TrustedIssuer(thumbprint, name));
            return this;
        }


        public IEnumerable<TrustedIssuer> TrustedIssuers
        {
            get { return _trustedIssuers; }
        }
        
        public Issuer WebServiceIssuer { get; private set; }
        
        public IRelyingPartyServerConfigurator ServiceIssuer(string issuerUrl)
        {
            WebServiceIssuer = new Issuer(issuerUrl,"");
            return this;
        }

        public Issuer WebIssuer { get; private set; }

        public IRelyingPartyServerConfigurator FederatedIssuer(string issuerUrl, string realmUrl)
        {
            WebIssuer = new Issuer(issuerUrl, realmUrl);
            return this;
        }


        IEnumerable<DisplayClaim> IRelyingPartyServerConfiguration.GetClaims()
        {
            return GetClaims();
        }

        public X509Certificate2 ServiceCertificate { get; private set; }

        public IRelyingPartyServerConfigurator Certificate(X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException("certificate");
            ServiceCertificate = certificate;
            return this;
        }


        readonly List<DisplayClaim> _claimCollection = new List<DisplayClaim>();

        public IRelyingPartyServerConfigurator Claim(string type, bool optional)
        {
            _claimCollection.Add(new DisplayClaim(type, null, null, null, optional));
            return this;
        }


        void IRelyingPartyServerConfigurator.Claims(IEnumerable<DisplayClaim> displayClaims)
        {
            _claimCollection.AddRange(displayClaims);
        }

        public IEnumerable<DisplayClaim> GetClaims()
        {
            return _claimCollection;
        }
    }
}