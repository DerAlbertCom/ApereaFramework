using System;
using System.Security.Cryptography.X509Certificates;

namespace Aperea.Identity.Settings
{
    public interface IEncryptionSettings
    {
        X509Certificate2 Certificate { get; }
        bool Encrypt { get; }
    }
}