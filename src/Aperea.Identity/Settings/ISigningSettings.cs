using System;
using System.Security.Cryptography.X509Certificates;

namespace Aperea.Identity.Settings
{
    public interface ISigningSettings
    {
        X509Certificate2 Certificate { get; }
        bool Sign { get; }
    }
}