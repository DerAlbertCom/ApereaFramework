using System.Web.Mvc;
using System.Collections.Generic;
using Aperea.MVC.PortableAreas;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.Controllers
{
    public class EmbeddedResourceController : Controller
    {
        public ActionResult Index(string resourceName, string resourcePath)
        {
            if (!string.IsNullOrEmpty(resourcePath))
            {
                resourceName = resourcePath + "." + resourceName;
            }

            var areaName = (string)this.RouteData.DataTokens["area"];
            var resourceStore = AssemblyResourceManager.GetResourceStoreForArea(areaName);
            // pre-pend "~" so that it will be replaced with assembly namespace
            var resourceStream = resourceStore.GetResourceStream("~." + resourceName);

            if (resourceStream == null)
            {
                this.Response.StatusCode = 404;
                return null;
            }

            var contentType = GetContentType(resourceName);
            return this.File(resourceStream, contentType);
        }

        #region Private Members

        private static string GetContentType(string resourceName)
        {
            var extension = resourceName.Substring(resourceName.LastIndexOf('.')).ToLower();
            return mimeTypes[extension];
        }

        private static readonly Dictionary<string, string> mimeTypes = InitializeMimeTypes();

        private static Dictionary<string, string> InitializeMimeTypes()
        {
            var mimes = new Dictionary<string, string>();
            mimes.Add(".gif", "image/gif");
            mimes.Add(".png", "image/png");
            mimes.Add(".jpg", "image/jpeg");
            mimes.Add(".js", "text/javascript");
            mimes.Add(".css", "text/css");
            mimes.Add(".txt", "text/plain");
            mimes.Add(".xml", "application/xml");
            mimes.Add(".zip", "application/zip");
            return mimes;
        }
        #endregion
    }
}
