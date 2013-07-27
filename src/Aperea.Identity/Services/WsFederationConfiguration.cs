using System;
using System.IdentityModel.Configuration;
using Aperea.Identity.Configuration;

namespace Aperea.Identity.Services
{
    public class WsFederationConfiguration : SecurityTokenServiceConfiguration
    {
        public WsFederationConfiguration()
        {
            IRelyingPartyServerConfiguration configuration = RelyingPartyServerConfiguration.Current;
            this.InitializeServiceConfiguration(configuration);
            SaveBootstrapContext = true;
            // .NET 4.5?
//            SaveBootstrapTokens = true;
        }
    }
}