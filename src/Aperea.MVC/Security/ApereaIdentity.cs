using System.Security.Principal;
using System.Web.Security;
using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Services;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.Security
{
    internal class ApereaIdentity : IIdentity
    {
        public ApereaIdentity()
        {
            Name = "";
            IsAuthenticated = false;
            AuthenticationType = "ApereaFramework";
        }

        public ApereaIdentity(FormsAuthenticationTicket ticket)
        {
            AuthenticationType = "ApereaFramework";
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
            AuthenticationType = "ApereaFramework";
        }

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}