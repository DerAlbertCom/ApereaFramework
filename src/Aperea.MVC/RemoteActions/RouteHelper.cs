using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aperea.MVC.RemoteActions
{
    public static class RouteHelper
    {
        static RouteValueDictionary GetRouteValues<T>(Expression<Action<T>> expression) where T : Controller
        {
            return Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(expression);
        }

        public static RedirectToRouteResult RedirectTo<T>(Expression<Action<T>> expression) where T : Controller
        {
            return new RedirectToRouteResult(GetRouteValues(expression));
        }
    }
}