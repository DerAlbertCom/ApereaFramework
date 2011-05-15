using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ApereaStart.UrlBuilders
{
    public class ActionUrlBuilder : IActionUrlBuilder
    {
        readonly HttpContextBase _httpContext;
        readonly HttpRequestBase _httpRequest;
        public ActionUrlBuilder(HttpContextBase httpContext, HttpRequestBase httpRequest)
        {
            _httpContext = httpContext;
            _httpRequest = httpRequest;
        }

        public string BuildActionUrl<TController>(Expression<Action<TController>> expression)
            where TController : Controller
        {
            return CreateUrl(GetRouteValues(expression));
        }

        static RouteValueDictionary GetRouteValues<TController>(Expression<Action<TController>> expression)
            where TController : Controller
        {
            return Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(expression);
        }

        string CreateUrl(RouteValueDictionary routeValues)
        {
            return GetBaseUrl() + GetUrlHelper().RouteUrl(routeValues);
        }

        UrlHelper GetUrlHelper()
        {
            return new UrlHelper(CreateRequestContext());
        }

        string GetBaseUrl()
        {
            return string.Format("{0}://{1}", _httpRequest.Url.Scheme, _httpRequest.Url.Authority);
        }

        RequestContext CreateRequestContext()
        {
            return new RequestContext(_httpContext, RouteTable.Routes.GetRouteData(_httpContext));
        }
    }
}