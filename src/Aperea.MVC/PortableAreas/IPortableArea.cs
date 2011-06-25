using System;

namespace Aperea.MVC.PortableAreas
{
    public interface IPortableArea
    {
        bool IsEmbeddedViewResourcePath(string virtualPath);
        PortableAreaStore GetResourceStoreFromVirtualPath(string virtualPath);
        PortableAreaStore GetResourceStoreForArea(string areaName);
        void RegisterAreaResources(Type areaAssemblyType, string routePrefix, string namespaceName);
    }
}