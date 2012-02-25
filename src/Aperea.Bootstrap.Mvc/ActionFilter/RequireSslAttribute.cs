using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Aperea.Settings;

namespace Aperea.ActionFilter
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
        Justification = "Unsealed because type contains virtual extensibility points.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RequireSslAttribute : RequireHttpsAttribute
    {
        const int DefaultHttpsPort = 443;

        public RequireSslAttribute(IApplicationSettings applicationSettings)
        {
            HttpsPort = applicationSettings.Get("WebServer.HttpsPort", () => DefaultHttpsPort);
            UrlFormat = HttpsPort == DefaultHttpsPort ? "https://{0}{1}" : "https://{0}:" + HttpsPort + "{1}";
        }

        public RequireSslAttribute() : this(new ApplicationSettings())
        {
        }

        public int HttpsPort { get; private set; }

        public string UrlFormat { get; private set; }


        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            if (!string.Equals(GetHttpMethod(filterContext), "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("");
            }
            var url = GetUrlWithOptionalHttpsPort(filterContext);
            filterContext.Result = new RedirectResult(url);
        }

        static string GetHttpMethod(AuthorizationContext filterContext)
        {
            return filterContext.HttpContext.Request.GetHttpMethodOverride().ToUpperInvariant();
        }

        string GetUrlWithOptionalHttpsPort(AuthorizationContext filterContext)
        {
            return string.Format(UrlFormat,
                                 filterContext.HttpContext.Request.Url.Host,
                                 filterContext.HttpContext.Request.RawUrl);
        }
    }
}