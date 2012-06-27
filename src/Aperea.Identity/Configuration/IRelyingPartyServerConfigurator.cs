using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aperea.Identity.Services;
using Microsoft.IdentityModel.Protocols.WSIdentity;

namespace Aperea.Identity.Configuration
{
    public interface IRelyingPartyServerConfigurator
    {
        IRelyingPartyServerConfigurator SetBaseUri(string url);
        IRelyingPartyServerConfigurator AudienceUri(string url);
        IRelyingPartyServerConfigurator ConfigureService<T>(string routePrefix, Action<IWebServiceServerConfigurator> configurator);
        IRelyingPartyServerConfigurator ConfigureService<T>(string routePrefix);
        IRelyingPartyServerConfigurator ServiceHostFactory(WsFederationServiceHostFactory wsFederationServiceHostFactory);
        IRelyingPartyServerConfigurator TrustedIssuer(string thumbprint, string name);
        IRelyingPartyServerConfigurator FederatedIssuer(string issuerUrl, string realmUrl);
        IRelyingPartyServerConfigurator ServiceIssuer(string issuerUrl);
        IRelyingPartyServerConfigurator Certificate(X509Certificate2 certificate);
        IRelyingPartyServerConfigurator Claim(string type, bool optional);
        void Claims(IEnumerable<DisplayClaim> displayClaims);
    }
}