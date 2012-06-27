using System;

namespace Aperea.Identity.Configuration
{
    public class TrustedIssuer
    {
        public TrustedIssuer(string thumbprint, string name)
        {
            Thumbprint = thumbprint;
            Name = name;
        }

        public string Thumbprint { get; private set; }

        public string Name { get; private set; }
    }
}