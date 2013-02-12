using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Aperea.Identity.Configuration;

namespace Aperea.Identity.Services
{
    public class WsIssuerNameRegistry : IssuerNameRegistry
    {
        readonly Dictionary<string, string> trustedIssuers = new Dictionary<string, string>();

        readonly IRelyingPartyServerConfiguration configuration;

        public WsIssuerNameRegistry():this(RelyingPartyServerConfiguration.Current)
        {
            foreach (var trustedIssuer in configuration.TrustedIssuers)
            {
                trustedIssuers.Add(trustedIssuer.Thumbprint.ToLowerInvariant(),trustedIssuer.Name);
            }
        }

        WsIssuerNameRegistry(IRelyingPartyServerConfiguration currentConfiguration)
        {
            configuration = currentConfiguration;
        }

        public override string GetIssuerName(SecurityToken securityToken)
        {
            if (securityToken == null)
                throw new ArgumentNullException("securityToken");

            var x509SecurityToken = securityToken as X509SecurityToken;
            if (x509SecurityToken == null)
                return null;

            string thumbprint = x509SecurityToken.Certificate.Thumbprint;

            if (thumbprint == null)
                return null;

            thumbprint = thumbprint .ToLowerInvariant();
            if (trustedIssuers.ContainsKey(thumbprint))
                return trustedIssuers[thumbprint];

            return null;
        }
    }
}