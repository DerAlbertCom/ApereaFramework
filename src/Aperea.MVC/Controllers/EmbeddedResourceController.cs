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

            var areaName = (string) RouteData.DataTokens["area"];
            var resourceStore = AssemblyResourceManager.GetResourceStoreForArea(areaName);
            // pre-pend "~" so that it will be replaced with assembly namespace
            var resourceStream = resourceStore.GetResourceStream("~." + resourceName);

            if (resourceStream == null)
            {
                return new HttpNotFoundResult();
            }

            var contentType = GetContentType(resourceName);
            return File(resourceStream, contentType);
        }

        static string GetContentType(string resourceName)
        {
            var extension = resourceName.Substring(resourceName.LastIndexOf('.')).ToLower();
            return MimeTypes[extension];
        }

        static readonly Dictionary<string, string> MimeTypes = InitializeMimeTypes();

        static Dictionary<string, string> InitializeMimeTypes()
        {
            var mimes = new Dictionary<string, string>
                        {
                            {".gif", "image/gif"},
                            {".png", "image/png"},
                            {".jpg", "image/jpeg"},
                            {".js", "text/javascript"},
                            {".css", "text/css"},
                            {".txt", "text/plain"},
                            {".xml", "application/xml"},
                            {".zip", "application/zip"}
                        };
            return mimes;
        }
    }
}