using System.Web.Mvc;
using System.Collections.Generic;
using Aperea.MVC.PortableAreas;
using Microsoft.Practices.ServiceLocation;

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

            var areaName = (string) RouteData.Values["area"];
            var resourceManager = ServiceLocator.Current.GetInstance<IPortableArea>();

            var resourceStore = resourceManager.GetResourceStoreForArea(areaName);
            var resourceStream = resourceStore.GetResourceStream("~." + resourceName);

            if (string.IsNullOrEmpty(resourceName))
            {
                return new HttpNotFoundResult();
            }

            return File(resourceStream, GetContentType(resourceName));
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