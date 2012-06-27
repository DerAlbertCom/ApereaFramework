using System;
using Aperea.Identity.Configuration;
using Aperea.Identity.Services;
using Microsoft.IdentityModel.Web;

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
            ServiceConfiguration.InitializeServiceConfiguration(configuration);
        }
    }
}