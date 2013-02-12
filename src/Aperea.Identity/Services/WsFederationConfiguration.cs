using System;
using System.IdentityModel.Configuration;
using Aperea.Identity.Configuration;

namespace Aperea.Identity.Services
{
    public class WsFederationConfiguration : SecurityTokenServiceConfiguration
    {
        readonly IRelyingPartyServerConfiguration _configuration;

        public WsFederationConfiguration()
        {
            _configuration = RelyingPartyServerConfiguration.Current;
//            this.InitializeServiceConfiguration(_configuration);
            SaveBootstrapContext = true;
//            SaveBootstrapTokens = true;
        }
    }
}