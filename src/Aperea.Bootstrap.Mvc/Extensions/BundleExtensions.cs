using System;
using System.Web.Mvc;

namespace Aperea.Extensions
{
    public static class BundleExtensions
    {
        public static string ResolveBundleUrl(this UrlHelper helper, string bundleVirtualPath)
        {
            return System.Web.Optimization.BundleTable.Bundles.ResolveBundleUrl(bundleVirtualPath);
        }

        public static string ResolveBundleUrl(this UrlHelper helper, string bundleVirtualPath, bool includeContentHash)
        {
            return System.Web.Optimization.BundleTable.Bundles.ResolveBundleUrl(bundleVirtualPath, includeContentHash);
        }
    }
}