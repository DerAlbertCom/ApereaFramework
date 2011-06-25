using System;
using System.Web.Hosting;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
    public class PortableAreaVirtualPathProvider : VirtualPathProvider
    {
        readonly IPortableArea _portableArea;

        public PortableAreaVirtualPathProvider(IPortableArea portableArea)
        {
            _portableArea = portableArea;
        }

        public override bool FileExists(string virtualPath)
        {
            bool exists = base.FileExists(virtualPath);
            return exists || _portableArea.IsEmbeddedViewResourcePath(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (_portableArea.IsEmbeddedViewResourcePath(virtualPath) && !base.FileExists(virtualPath))
            {
                var resourceStore = _portableArea.GetResourceStoreFromVirtualPath(virtualPath);
                return new PortableAreaVirtualFile(virtualPath, resourceStore);
            }
            return base.GetFile(virtualPath);
        }

        public override System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath,
                                                                              System.Collections.IEnumerable
                                                                                  virtualPathDependencies,
                                                                              DateTime utcStart)
        {
            if (_portableArea.IsEmbeddedViewResourcePath(virtualPath))
            {
                return null;
            }
            return base.GetCacheDependency(virtualPath, new string[0], utcStart);
        }

        public override string GetCacheKey(string virtualPath)
        {
            return null;
        }
    }
}