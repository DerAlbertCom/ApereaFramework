using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.WSIdentity;

namespace Aperea.Identity.Configuration
{
    public interface IRelyingPartyServerConfiguration 
    {
        string BaseUrl { get; }
        IEnumerable<Uri> AudienceUris { get; }
        Issuer WebServiceIssuer { get; }
        Issuer WebIssuer { get; }
        IEnumerable<DisplayClaim> GetClaims();
        X509Certificate2 ServiceCertificate { get; }
        IEnumerable<TrustedIssuer> TrustedIssuers { get; }
    }
}