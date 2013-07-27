using System;
using System.Security.Cryptography.X509Certificates;

namespace Aperea.Identity.Settings
{
    public interface ICertificateSettings
    {
        string SigningCertificateName { get; }
        string EncryptingCertificateName { get; }
        StoreName StoreName { get; }
        StoreLocation StoreLocation { get; }
    }
}