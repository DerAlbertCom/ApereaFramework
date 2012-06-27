using System;

namespace Aperea.Identity.Configuration
{
    public class Issuer
    {
        public Issuer(Uri issuer, string realm)
        {
            Uri = issuer;
            Realm = realm;
        }

        public Issuer(string issuer, string realm):this(new Uri(issuer),realm )
        {
            
        }

        public Uri Uri { get; private set; }
        public string Realm { get; private set; }
    }
}