using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Aperea.Identity.Configuration;

namespace Aperea.Identity.Services
{
    public class WsIssuerNameRegistry : IssuerNameRegistry
    {
        readonly Dictionary<string, string> _trustedIssuers = new Dictionary<string, string>();

        readonly IRelyingPartyServerConfiguration _configuration;

        public WsIssuerNameRegistry():this(RelyingPartyServerConfiguration.Current)
        {
            foreach (var trustedIssuer in _configuration.TrustedIssuers)
            {
                _trustedIssuers.Add(trustedIssuer.Thumbprint.ToLowerInvariant(),trustedIssuer.Name);
            }
        }

        WsIssuerNameRegistry(IRelyingPartyServerConfiguration currentConfiguration)
        {
            _configuration = currentConfiguration;
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
            if (_trustedIssuers.ContainsKey(thumbprint))
                return _trustedIssuers[thumbprint];

            return null;
        }
    }
}