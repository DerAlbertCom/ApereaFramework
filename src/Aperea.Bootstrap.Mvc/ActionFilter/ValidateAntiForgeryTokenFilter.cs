using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using Aperea.Attributes;

namespace Aperea.ActionFilter
{
    public class ValidateAntiForgeryTokenFilter : IAuthorizationFilter
    {
        readonly ValidateAntiForgeryTokenAttribute validateAntiForgery = new ValidateAntiForgeryTokenAttribute();

        static readonly Collection<string> _validatingMethods = new Collection<string> {"POST", "DELETE", "PUT"};

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var method = GetHttpMethod(filterContext);
            if (method == "GET")
            {
                return;
            }

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof (DontValidateAntiforgeryTokenAttribute), true).Any())
            {
                return;
            }

            if (_validatingMethods.Contains(method))
            {
                validateAntiForgery.OnAuthorization(filterContext);
            }
        }

        static string GetHttpMethod(AuthorizationContext filterContext)
        {
            return filterContext.HttpContext.Request.GetHttpMethodOverride().ToUpperInvariant();
        }

        public int Order
        {
            get { return validateAntiForgery.Order; }
            set { validateAntiForgery.Order = value; }
        }
    }
}