using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.WSIdentity;

namespace Aperea.Identity
{
    public interface IRelyingPartyConfiguration
    {
        string IssuerUri { get; }
        X509Certificate2 EncryptionCertificate { get; }
        bool Encrypt { get; }
        bool Sign { get; }
        X509Certificate2 SigningCertificate { get; }
        IEnumerable<string> GetPassiveRequestorEndpoints();
        IEnumerable<DisplayClaim> GetClaimTypsRequested();
        IEnumerable<string> GetSecurityTokenServiceEndpoints();
    }
}