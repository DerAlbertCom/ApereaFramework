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
        private static Dictionary<string, AssemblyResourceStore> assemblyResourceStores = InitializeAssemblyResourceStores();

        private static Dictionary<string, AssemblyResourceStore> InitializeAssemblyResourceStores()
        {
            var resourceStores = new Dictionary<string, AssemblyResourceStore>();

            // Add default AssemblyResourceStore for input builders
            var inputBuildersStore = new AssemblyResourceStore(typeof(AssemblyResourceProvider), "/views/inputbuilders", "MvcContrib.UI.InputBuilder.Views.InputBuilders");
            resourceStores.Add(inputBuildersStore.VirtualPath, inputBuildersStore);

            return resourceStores;
        }

        public static AssemblyResourceStore GetResourceStoreForArea(string areaName)
        {
            return assemblyResourceStores["/areas/" + areaName.ToLower()];
        }

        public static AssemblyResourceStore GetResourceStoreFromVirtualPath(string virtualPath)
        {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath).ToLower();
            foreach (var resourceStore in assemblyResourceStores)
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
            assemblyResourceStores.Add(assemblyResourceStore.VirtualPath, assemblyResourceStore);
        }
    }
}