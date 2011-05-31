using System.Security.Principal;
using Aperea.EntityModels;

namespace Aperea.Security
{
    internal class ApereaIdentity : IIdentity
    {
        public ApereaIdentity()
        {
            Name = "";
            IsAuthenticated = false;
            AuthenticationType = "ApereaFramework";
        }

        public ApereaIdentity(Login login)
        {
            Name = login.Loginname;
            IsAuthenticated = true;
            AuthenticationType = "ApereaFramework";
        }

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}