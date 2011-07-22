using System;
using System.Web;
using System.Web.Security;

namespace Aperea.MVC.Security
{
    public static class ApereaFormsAuthentication
    {
        const int TicketVersion = 4711;

        public static string FormsCookieName
        {
            get { return FormsAuthentication.FormsCookieName + "_Aperea"; }
        }

        public static void SignOn(string loginName, bool rememberMe)
        {
            var authTicket = new FormsAuthenticationTicket(TicketVersion, loginName, DateTime.Now,
                                                           DateTime.Now.Add(FormsAuthentication.Timeout),
                                                           rememberMe,
                                                           string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsCookieName, encryptedTicket)
                             {
                                 Secure = FormsAuthentication.RequireSSL
                             };
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        public static void SignOut()
        {
            HttpContext.Current.Response.Cookies.Remove(FormsCookieName);
            var cookie = new HttpCookie(FormsCookieName, string.Empty);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}