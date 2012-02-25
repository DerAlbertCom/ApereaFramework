using System;
using System.Security.Cryptography.X509Certificates;
using Aperea.Settings;

namespace Aperea.Identity.Settings
{
    public interface ICertificateSettings
    {
        string SigningCertificateName { get; }
        string EncryptingCertificateName { get; }
        StoreName StoreName { get; }
        StoreLocation StoreLocation { get; }
    }

    public class CertificateSettings : ICertificateSettings
    {
        readonly IApplicationSettings settings;

        public CertificateSettings(IApplicationSettings settings)
        {
            this.settings = settings;
        }

        public string SigningCertificateName
        {
            get { return settings.Get<string>("Certificate.SigningCertificateName"); }
        }

        public string EncryptingCertificateName
        {
            get { return settings.Get<string>("Certificate.EncryptingCertificateName"); }
        }

        public StoreName StoreName
        {
            get { return settings.Get("Certificate.StoreName", () => StoreName.My); }
        }

        public StoreLocation StoreLocation
        {
            get { return settings.Get("Certificate.StoreLocation", () => StoreLocation.CurrentUser); }
        }
    }
}