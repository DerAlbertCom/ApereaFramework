using System;
using System.Collections.Generic;
using System.Web;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
    /// <summary>
    /// Manages all .NET assemblies that have registered their embedded resources.
    /// </summary>
    public  class PortableArea : IPortableArea
    {
        readonly Dictionary<string, PortableAreaStore> _assemblyResourceStores;

        public PortableArea()
        {
             _assemblyResourceStores = new Dictionary<string, PortableAreaStore>();
            
        }

        public PortableAreaStore GetResourceStoreForArea(string areaName)
        {
            return _assemblyResourceStores[areaName+"/".ToLower()];
        }

        public void RegisterAreaResources(Type areaAssemblyType, string routePrefix, string namespaceName)
        {            
            var resourceStore = new PortableAreaStore(areaAssemblyType, routePrefix+"/", namespaceName);
            _assemblyResourceStores.Add(resourceStore.VirtualPath, resourceStore);
        }

        public PortableAreaStore GetResourceStoreFromVirtualPath(string virtualPath)
        {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath).ToLower();
            foreach (var resourceStore in _assemblyResourceStores)
            {
                if (checkPath.Contains(resourceStore.Key) && resourceStore.Value.IsPathResourceStream(checkPath))
                {
                    return resourceStore.Value;
                }
            }
            return null;
        }

        public  bool IsEmbeddedViewResourcePath(string virtualPath)
        {
            var resourceStore = GetResourceStoreFromVirtualPath(virtualPath);
            return (resourceStore != null);
        }
    }
}