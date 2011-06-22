using System;
using System.Collections.Generic;
using System.IO;

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
            if (!path.StartsWith("~/areas/",StringComparison.InvariantCultureIgnoreCase))
                return string.Empty;
            path = RemoveFirst(path, "areas/");
            path = RemoveFirst(path, VirtualPath);

            var resourceName = path.Replace("~", namespaceName).ToLower();
            return resourceName.Replace("/", ".");
        }

        static string RemoveFirst(string path, string urlPart)
        {
            var pos = path.IndexOf(urlPart, StringComparison.InvariantCultureIgnoreCase);
            if (pos > 0)
            {
                path = path.Remove(pos, urlPart.Length);
            }
            return path;
        }

        public bool IsPathResourceStream(string path)
        {
            var fullResourceName = GetFullyQualifiedTypeFromPath(path);
            return resources.ContainsKey(fullResourceName);
        }
    }
}