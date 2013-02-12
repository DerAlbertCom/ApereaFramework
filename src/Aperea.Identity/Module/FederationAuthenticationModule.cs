using System;
using System.IdentityModel.Services;
using System.ServiceModel;
using Aperea.Identity.Configuration;

namespace Aperea.Identity.Module
{
    public class FederationAuthenticationModule : WSFederationAuthenticationModule
    {
        protected override void InitializeModule(System.Web.HttpApplication context)
        {
            base.InitializeModule(context);
            var configuration = RelyingPartyServerConfiguration.Current;
            PassiveRedirectEnabled = true;
            if (configuration.WebIssuer != null)
            {
                Issuer = configuration.WebIssuer.Uri.OriginalString;
                Realm = configuration.WebIssuer.Realm;
            }
            RequireHttps = true;
//            ServiceConfiguration.InitializeServiceConfiguration(configuration);
        }
    }
}