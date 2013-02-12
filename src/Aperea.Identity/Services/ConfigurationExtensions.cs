using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using Aperea.Identity.Configuration;
using Aperea.Identity.Settings;
using Aperea.Settings;

namespace Aperea.Identity.Services
{
    static internal class ConfigurationExtensions
    {
        internal static void InitializeServiceConfiguration(this ServiceConfiguration serviceConfiguration, IRelyingPartyServerConfiguration configuration)
        {
            var settings = new CertificateSettings(new ApplicationSettings());
            //serviceConfiguration.ServiceTokenResolver = new X509CertificateStoreTokenResolver(settings.StoreName, settings.StoreLocation);

            //serviceConfiguration.IssuerNameRegistry = new WsIssuerNameRegistry();
            //serviceConfiguration.ServiceCertificate = configuration.ServiceCertificate;
            //serviceConfiguration.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
            //serviceConfiguration.RevocationMode = X509RevocationMode.NoCheck;

            //var audienceRestriction = serviceConfiguration.AudienceRestriction;
            //foreach (var audienceUri in configuration.AudienceUris)
            //{
            //    if (!audienceRestriction.AllowedAudienceUris.Contains(audienceUri))
            //        audienceRestriction.AllowedAudienceUris.Add(audienceUri);
            //}
        }
    }
}