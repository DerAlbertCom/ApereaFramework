using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Aperea.Identity.Settings
{
    internal class CertificateStore
    {
        readonly ICertificateSettings settings;
        readonly Lazy<X509Certificate2> certificate;

        public CertificateStore(ICertificateSettings settings, string certificateName)
        {
            this.settings = settings;
            certificate = new Lazy<X509Certificate2>(() => GetCertificate(certificateName));
        }

        public X509Certificate2 Certificate
        {
            get { return certificate.Value; }
        }

        X509Certificate2 GetCertificate(string certName)
        {
            if (string.IsNullOrWhiteSpace(certName))
                return null;

            return GetCertificate(settings.StoreName, settings.StoreLocation, certName);
        }

        static X509Certificate2 GetCertificate(StoreName name, StoreLocation location, string subjectName)
        {
            var store = new X509Store(name, location);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certificates = store.Certificates;
            try
            {
                var certs =
                    store.Certificates.Cast<X509Certificate2>().Where(cert => cert.SubjectName.Name == subjectName).
                        ToList();

                if (certs.Count > 1)
                {
                    throw new ApplicationException(string.Format(
                        "There are multiple certificates for subject Name {0}", subjectName));
                }
                if (certs.Count == 0)
                {
                    throw new ApplicationException(string.Format("No certificate was found for subject Name {0}",
                                                                 subjectName));
                }
                return certs.First();
            }
            finally
            {
                foreach (X509Certificate2 cert in certificates)
                {
                    cert.Reset();
                }

                store.Close();
            }
        }
    }
}