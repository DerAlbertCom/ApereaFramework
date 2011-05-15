using System;
using System.Web;
using System.Web.Mvc;

// based on a solution from Scott Hanselman
// http://www.hanselman.com/blog/ABetterASPNETMVCMobileDeviceCapabilitiesViewEngine.aspx

namespace Aperea.MVC.Views
{
    public static class AdaptiveViewEngineHelpers
    {
        private static bool UserAgentContains(this HttpContextBase c, string agentToFind)
        {
            return (c.Request.UserAgent.IndexOf(agentToFind, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static bool IsMobileDevice(this HttpContextBase c)
        {
            return c.Request.Browser.IsMobileDevice;
        }

        public static void AddMobile<T>(this ViewEngineCollection viewEngines, Func<HttpContextBase, bool> isTheRightDevice, string pathToSearch)
            where T : IViewEngine, new()
        {
            viewEngines.Add(new AdaptiveViewEngine(isTheRightDevice, pathToSearch, new T()));
        }

        public static void AddMobile<T>(this ViewEngineCollection viewEngines, string userAgentSubstring, string pathToSearch)
            where T : IViewEngine, new()
        {
            viewEngines.Add(new AdaptiveViewEngine(c => c.UserAgentContains(userAgentSubstring), pathToSearch, new T()));
        }

        public static void AddIPhone<T>(this ViewEngineCollection viewEngines) //specific example helper
            where T : IViewEngine, new()
        {
            viewEngines.Add(new AdaptiveViewEngine(c => c.UserAgentContains("iPhone"), "Mobile/iPhone", new T()));
        }

        public static void AddIPad<T>(this ViewEngineCollection viewEngines) //specific example helper
    where T : IViewEngine, new()
        {
            viewEngines.Add(new AdaptiveViewEngine(c => c.UserAgentContains("iPad"), "Mobile/iPad", new T()));
        }

        public static void AddGenericMobile<T>(this ViewEngineCollection viewEngines)
            where T : IViewEngine, new()
        {
            viewEngines.Add(new AdaptiveViewEngine(c => c.IsMobileDevice(), "Mobile", new T()));
        }
    }
}