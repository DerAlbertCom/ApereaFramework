using System;
using Aperea.Identity.Configuration;
using Microsoft.IdentityModel.Configuration;

namespace Aperea.Identity.Services
{
    public class WsFederationConfiguration : SecurityTokenServiceConfiguration
    {
        readonly IRelyingPartyServerConfiguration configuration;

        public WsFederationConfiguration()
        {
            configuration = RelyingPartyServerConfiguration.Current;
            this.InitializeServiceConfiguration(configuration);

            SaveBootstrapTokens = true;
        }
    }
}