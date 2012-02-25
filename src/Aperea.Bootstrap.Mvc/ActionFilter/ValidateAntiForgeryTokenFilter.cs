using System;
using System.Collections.ObjectModel;
using System.Web.Mvc;

namespace Aperea.ActionFilter
{
    public class ValidateAntiForgeryTokenFilter : IAuthorizationFilter
    {
        readonly ValidateAntiForgeryTokenAttribute validateAntiForgery = new ValidateAntiForgeryTokenAttribute();

        static readonly Collection<string> validatingMethods = new Collection<string> {"POST", "DELETE", "PUT"};

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var method = GetHttpMethod(filterContext);
            if (method == "GET")
            {
                return;
            }

            if (validatingMethods.Contains(method))
            {
                validateAntiForgery.OnAuthorization(filterContext);
            }
        }

        static string GetHttpMethod(AuthorizationContext filterContext)
        {
            return filterContext.HttpContext.Request.GetHttpMethodOverride().ToUpperInvariant();
        }

        public string Salt
        {
            get { return validateAntiForgery.Salt; }
            set { validateAntiForgery.Salt = value; }
        }

        public int Order
        {
            get { return validateAntiForgery.Order; }
            set { validateAntiForgery.Order = value; }
        }
    }
}