using System;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using Aperea.Initialize;
using Aperea.MVC.Security;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.Initialize
{
    public static class MvcApplicationStart
    {
        public static void Initialize(HttpApplication httpApplication)
        {
            httpApplication.AuthenticateRequest += OnAuthenticateRequest;
            ApplicationStart.Initialize();
        }

        static T GetInstance<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        static HttpContextBase Context
        {
            get { return GetInstance<HttpContextBase>(); }
        }

        static void OnAuthenticateRequest(object sender, EventArgs eventArgs)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (null == authCookie)
            {
                return;
            }

            SetPrincipal(authCookie);
        }

        static void SetPrincipal(HttpCookie authCookie)
        {
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                return;
            }
            Context.User = new ApereaPrincipal(new FormsIdentity(authTicket));
        }
    }
}