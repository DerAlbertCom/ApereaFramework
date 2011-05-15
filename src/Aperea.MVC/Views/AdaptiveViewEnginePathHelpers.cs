using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

// based on a solution from Scott Hanselman
// http://www.hanselman.com/blog/ABetterASPNETMVCMobileDeviceCapabilitiesViewEngine.aspx

namespace Aperea.MVC.Views
{
    public static class AdaptiveViewEnginePathHelpers
    {
        private static string GetNewLayoutPath(HttpContextBase context, IAdaptiveViewEngine viewEngine,
                                              string layoutFile)
        {
            if (!viewEngine.IsTheRightDevice(context))
                return String.Empty;

            string path = context.Server.MapPath(layoutFile);

            path = GetFullPathWithSearchPath(path, viewEngine.PathToSearch);
            if (!File.Exists(path))
                return String.Empty;
            return MakeVirtualPath(path);
        }

        private static string GetFullPathWithSearchPath(string path, string pathToSearch)
        {
            string fileName = Path.GetFileName(path);
            path = Path.GetDirectoryName(path);
            path = Path.Combine(path, pathToSearch);
            path = Path.Combine(path, fileName);
            return path;
        }

        private static string MakeVirtualPath(string path)
        {
            var physicalPath = HostingEnvironment.ApplicationPhysicalPath;
            if (path.StartsWith(path)){
                path = path.Substring(physicalPath.Length);
            }
            path = path.Replace(@"\", "/");
            path = path.Insert(0, !path.StartsWith("/") ? "~/" : "~");
            return path;
        }

        private static IEnumerable<IAdaptiveViewEngine> GetViewEngines()
        {
            return ViewEngines.Engines.Where(ve => ve is IAdaptiveViewEngine).Cast<IAdaptiveViewEngine>();
        }

        public static string GetLayoutPath(this HttpContextBase httpContext, string layoutFile)
        {
            foreach (var viewEngine in GetViewEngines()){
                var newLayout = GetNewLayoutPath(httpContext, viewEngine, layoutFile);
                if (!String.IsNullOrEmpty(newLayout)){
                    return newLayout;
                }
            }
            return layoutFile;
        }
    }
}