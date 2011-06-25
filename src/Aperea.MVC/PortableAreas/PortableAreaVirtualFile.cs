using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Hosting;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
	public class PortableAreaVirtualFile : VirtualFile
	{
        private readonly PortableAreaStore _resourceStore;
        private readonly string _path;

        public PortableAreaVirtualFile(string virtualPath, PortableAreaStore resourceStore)
            : base(virtualPath)
        {
            _resourceStore = resourceStore;
            _path = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            Trace.WriteLine("Opening embedded view" + _path);
            return _resourceStore.GetResourceStream(_path);
        }
	}
}