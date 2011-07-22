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

        ApereaIdentity()
        {
            Name = "";
            IsAuthenticated = false;
            AuthenticationType = AuthenticationName;
        }

        public ApereaIdentity(FormsAuthenticationTicket ticket):this()
        {
            IsAuthenticated = IsValidLogin(ticket.Name);

            if (IsAuthenticated)
            {
                Name = ticket.Name;
            }
        }

        public ApereaIdentity(Login login):this()
        {
            Name = login.Loginname;
            IsAuthenticated = login.Active;
        }
        
        bool IsValidLogin(string loginName)
        {
            var validation = ServiceLocator.Current.GetInstance<ILoginValidation>();
            return validation.IsValidLogin(loginName);
        }

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}