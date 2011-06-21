using System.Collections.Generic;
using System.Web;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
    /// <summary>
    /// Manages all .NET assemblies that have registered their embedded resources.
    /// </summary>
    public static class AssemblyResourceManager
    {
        private static readonly Dictionary<string, AssemblyResourceStore> AssemblyResourceStores = InitializeAssemblyResourceStores();

        private static Dictionary<string, AssemblyResourceStore> InitializeAssemblyResourceStores()
        {
            var resourceStores = new Dictionary<string, AssemblyResourceStore>();

            return resourceStores;
        }

        public static AssemblyResourceStore GetResourceStoreForArea(string areaName)
        {
            return AssemblyResourceStores[areaName.ToLower()];
        }

        public static AssemblyResourceStore GetResourceStoreFromVirtualPath(string virtualPath)
        {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath).ToLower();
            foreach (var resourceStore in AssemblyResourceStores)
            {
                if (checkPath.Contains(resourceStore.Key) && resourceStore.Value.IsPathResourceStream(checkPath))
                {
                    return resourceStore.Value;
                }
            }
            return null;
        }

        public static bool IsEmbeddedViewResourcePath(string virtualPath)
        {
            var resourceStore = GetResourceStoreFromVirtualPath(virtualPath);
            return (resourceStore != null);
        }

        public static void RegisterAreaResources(AssemblyResourceStore assemblyResourceStore)
        {
            AssemblyResourceStores.Add(assemblyResourceStore.VirtualPath, assemblyResourceStore);
        }
    }
}