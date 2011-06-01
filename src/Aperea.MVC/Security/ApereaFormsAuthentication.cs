using System;
using System.Web;
using System.Web.Security;

namespace Aperea.MVC.Security
{
    public class ApereaFormsAuthentication
    {
        public const int TicketVersion = 4711;

        public static void SignOn(string username,  bool rememberMe)
        {
            var authTicket = new FormsAuthenticationTicket(TicketVersion, username, DateTime.Now,
                                                           DateTime.Now.Add(FormsAuthentication.Timeout),
                                                           rememberMe,
                                                           string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}