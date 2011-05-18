using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Aperea.Settings;

namespace Aperea.MVC.ActionFilter
{
    /// <summary>
    /// Set the CurrentThread Culture to the given culture in the url. if no culture is
    /// given a possible culture will detected from the browser settings in
    /// consideration with the possible languages from the settings.
    /// When no language can be found the culture will set to the default culture
    /// </summary>
    public class ThreadCultureAttribute : DependencyFilterAttribute, IActionFilter
    {
        public ThreadCultureAttribute()
        {
            Order = -1;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cultureName = GetCurrentCulture(filterContext);
            var ci = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        string GetCurrentCulture(ControllerContext filterContext)
        {
            var cultureName = filterContext.RouteData.Values["culture"] as string;
            var settings = GetService<ICultureSettings>();
            var possibleCultures = settings.PossibleCultures;

            if (string.IsNullOrEmpty(cultureName) || !possibleCultures.Contains(cultureName))
            {
                var userCultures = GetUserCulture(filterContext);

                cultureName = FindSpecificUserCulture(userCultures);

                if (string.IsNullOrEmpty(cultureName))
                {
                    cultureName = FindNeutralUserCulture(userCultures);
                }

                if (string.IsNullOrEmpty(cultureName))
                {
                    cultureName = possibleCultures.Length > 0 ? possibleCultures[0] : settings.DefaultCulture;
                }
                filterContext.RouteData.Values["culture"] = cultureName;
            }
            return cultureName;
        }

        string FindSpecificUserCulture(IEnumerable<string> userCultures)
        {
            return SearchInPossibleCultures(userCultures, culture => culture);
        }

        string FindNeutralUserCulture(IEnumerable<string> userCultures)
        {
            return SearchInPossibleCultures(userCultures, culture => culture.Split('-')[0]);
        }

        string SearchInPossibleCultures(IEnumerable<string> userCultures, Func<string, string> modifier)
        {
            var settings = DependencyResolver.Current.GetService<ICultureSettings>();
            var possibleCultures = settings.PossibleCultures;
            foreach (var culture in userCultures)
            {
                var modifiedCulture = modifier(culture);
                if (possibleCultures.Any(p => p == modifiedCulture))
                {
                    return modifiedCulture;
                }
            }
            return string.Empty;
        }

        IEnumerable<string> GetUserCulture(ControllerContext filterContext)
        {
            var userLanguages = filterContext.HttpContext.Request.UserLanguages;
            return userLanguages ?? new string[0];
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}