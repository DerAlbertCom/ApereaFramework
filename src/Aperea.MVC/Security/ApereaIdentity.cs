using System.Security.Principal;
using System.Web.Security;
using Aperea.EntityModels;
using Aperea.Services;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.Security
{
    internal class ApereaIdentity : IIdentity
    {
        const string AuthenticationName = "ApereaFramework";

        public ApereaIdentity()
        {
            Name = "";
            IsAuthenticated = false;
            AuthenticationType = AuthenticationName;
        }

        public ApereaIdentity(FormsAuthenticationTicket ticket)
        {
            AuthenticationType = AuthenticationName;
            IsAuthenticated = IsValidLogin(ticket.Name);

            if (IsAuthenticated)
            {
                Name = ticket.Name;
            }
        }

        bool IsValidLogin(string loginName)
        {
            var validation = ServiceLocator.Current.GetInstance<ILoginValidation>();
            return validation.IsValidLogin(loginName);
        }

        public ApereaIdentity(Login login)
        {
            Name = login.Loginname;
            IsAuthenticated = true;
            AuthenticationType = AuthenticationName;
        }

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}