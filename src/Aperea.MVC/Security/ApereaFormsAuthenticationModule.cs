using System;
using System.Web;
using System.Web.Security;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.Security
{
    public class ApereaFormsAuthenticationModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnAuthenticateRequest;
            context.EndRequest += OnEndRequest;
        }

        public void Dispose()
        {
        }


        private void OnEndRequest(object sender, EventArgs eventArgs)
        {
            HttpContext context = ((HttpApplication) sender).Context;
            if (context.Response.StatusCode == 401)
            {
                string returnUrl = context.Request.QueryString["ReturnUrl"];
                string loginUrl = FormsAuthentication.LoginUrl;
                if (string.IsNullOrWhiteSpace(loginUrl))
                {
                    throw new HttpException("Invalid FormsAuthentication.LoginUrl");
                }
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    loginUrl += loginUrl.Contains("?") ? "&" : "?";
                    loginUrl += "ReturnUrl=" + HttpUtility.UrlEncode(returnUrl);
                }
                context.Response.Redirect(loginUrl, false);
            }
        }

        private T GetInstance<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        private HttpContextBase Context
        {
            get { return GetInstance<HttpContextBase>(); }
        }

        private void OnAuthenticateRequest(object sender, EventArgs eventArgs)
        {
            HttpCookie authCookie = Context.Request.Cookies[ApereaFormsAuthentication.FormsCookieName];

            if (null == authCookie)
            {
                return;
            }

            SetPrincipal(authCookie);
        }

        private void SetPrincipal(HttpCookie authCookie)
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
            if (authTicket == null)
                return;

            Context.User = new ApereaPrincipal(new FormsIdentity(authTicket));
        }
    }
}