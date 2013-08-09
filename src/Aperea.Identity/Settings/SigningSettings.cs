using System.Security.Cryptography.X509Certificates;

namespace Aperea.Identity.Settings
{
    public class SigningSettings : ISigningSettings
    {
        readonly CertificateStore certificateStore;

        public SigningSettings(ICertificateSettings settings)
        {
            certificateStore = new CertificateStore(settings, settings.SigningCertificateName);
        }


        public X509Certificate2 Certificate
        {
            get { return certificateStore.Certificate; }
        }

        public bool Sign
        {
            get { return Certificate != null; }
        }
    }
}