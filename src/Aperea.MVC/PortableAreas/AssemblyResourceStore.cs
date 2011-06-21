using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
    /// <summary>
    /// Stores all the embedded resources for a single assembly/area.
    /// </summary>
    public class AssemblyResourceStore
    {
        Dictionary<string, string> resources;
        Type typeToLocateAssembly;
        string namespaceName;

        public string VirtualPath { get; private set; }

        public AssemblyResourceStore(Type typeToLocateAssembly, string virtualPath, string namespaceName)
        {
            Initialize(typeToLocateAssembly, virtualPath, namespaceName);
        }

        void Initialize(Type typeToLocateAssembly, string virtualPath, string @namespace)
        {
            this.typeToLocateAssembly = typeToLocateAssembly;
            // should we disallow an empty virtual path?
            this.VirtualPath = virtualPath.ToLower();
            this.namespaceName = @namespace.ToLower();

            var resourceNames = this.typeToLocateAssembly.Assembly.GetManifestResourceNames();
            resources = new Dictionary<string, string>(resourceNames.Length);
            foreach (var name in resourceNames)
            {
                resources.Add(name.ToLower(), name);
            }
        }

        public Stream GetResourceStream(string resourceName)
        {
            var fullResourceName = GetFullyQualifiedTypeFromPath(resourceName);

            string actualResourceName = null;

            if (resources.TryGetValue(fullResourceName, out actualResourceName))
            {
                Stream stream = this.typeToLocateAssembly.Assembly.GetManifestResourceStream(actualResourceName);
                return stream;
            }
            else
            {
                return null;
            }
        }

        public string GetFullyQualifiedTypeFromPath(string path)
        {
            var fileName = Path.GetFileName(path);
            var endController = path.LastIndexOf(fileName)-1;
            if (endController < 0)
                return path;
            path = path.Substring(0, endController);
            var startController = path.LastIndexOf("/");
            if (startController < 0)
                return path;
            var controller = path.Substring(startController + 1);
            string resourceName = string.Format("{0}.views.{1}.{2}", namespaceName, controller, fileName);
            return resourceName.Replace("/", ".");
        }

        public bool IsPathResourceStream(string path)
        {
            var fullResourceName = GetFullyQualifiedTypeFromPath(path);
            return resources.ContainsKey(fullResourceName);
        }
    }
}