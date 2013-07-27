using System.IdentityModel.Configuration;
using System.IdentityModel.Services.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using Aperea.Identity.Configuration;
using Aperea.Identity.Services;
using Aperea.Identity.Settings;
using Aperea.Settings;

namespace Aperea.Identity
{
    static internal class ConfigurationExtensions
    {
        public static void InitializeServiceConfiguration(this IdentityConfiguration serviceConfiguration, IRelyingPartyServerConfiguration configuration)
        {
            var settings = new CertificateSettings(new ApplicationSettings());
            serviceConfiguration.ServiceTokenResolver = new X509CertificateStoreTokenResolver(settings.StoreName, settings.StoreLocation);

            serviceConfiguration.IssuerNameRegistry = new WsIssuerNameRegistry();
            serviceConfiguration.ServiceCertificate = configuration.ServiceCertificate;
            serviceConfiguration.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
            serviceConfiguration.RevocationMode = X509RevocationMode.NoCheck;

            var audienceRestriction = serviceConfiguration.AudienceRestriction;
            foreach (var audienceUri in configuration.AudienceUris)
            {
                if (!audienceRestriction.AllowedAudienceUris.Contains(audienceUri))
                    audienceRestriction.AllowedAudienceUris.Add(audienceUri);
            }
        }

        public static void InitializeServiceConfiguration(FederationConfiguration federationConfiguration)
        {
            throw new System.NotImplementedException();
        }
    }
}