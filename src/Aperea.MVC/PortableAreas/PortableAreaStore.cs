using System;
using System.Collections.Generic;
using System.IO;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
    /// <summary>
    /// Stores all the embedded resources for a single assembly/area.
    /// </summary>
    public sealed class PortableAreaStore
    {
        Dictionary<string, string> _resources;
        Type _assemblyWithViewType;
        string _namespaceName;

        public string VirtualPath { get; private set; }

        internal PortableAreaStore(Type typeToLocateAssembly, string virtualPath, string namespaceName)
        {
            Initialize(typeToLocateAssembly, virtualPath, namespaceName);
        }

        void Initialize(Type assemblyWithViewType, string virtualPath, string namespaceName)
        {
            _assemblyWithViewType = assemblyWithViewType;
            VirtualPath = virtualPath.ToLower();
            _namespaceName = namespaceName.ToLower();

            var resourceNames = _assemblyWithViewType.Assembly.GetManifestResourceNames();
            _resources = new Dictionary<string, string>(resourceNames.Length);
            foreach (var name in resourceNames)
            {
                _resources.Add(name.ToLower(), name);
            }
        }

        public Stream GetResourceStream(string resourceName)
        {
            var fullResourceName = GetFullyQualifiedTypeFromPath(resourceName);

            string actualResourceName;

            if (_resources.TryGetValue(fullResourceName, out actualResourceName))
            {
                return _assemblyWithViewType.Assembly.GetManifestResourceStream(actualResourceName);
            }
            return null;
        }

        string GetFullyQualifiedTypeFromPath(string path)
        {
            if (!path.StartsWith("~/areas/",StringComparison.InvariantCultureIgnoreCase))
                return string.Empty;
            path = RemoveFirst(path,"areas/");
            path = RemoveFirst(path, VirtualPath);

            var resourceName = path.Replace("~", _namespaceName).ToLower();
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
            return _resources.ContainsKey(fullResourceName);
        }
    }
}