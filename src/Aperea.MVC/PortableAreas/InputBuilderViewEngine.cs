using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
	public class InputBuilderViewEngine : RazorViewEngine
	{
		public InputBuilderViewEngine(string[] subdirs)
		{
			IEnumerable<string> inputs = subdirs.Concat(new[] {"InputBuilders"});

			PartialViewLocationFormats =
				inputs.Select(s => "~/Views/" + s + "/{0}.cshtml").Concat(subdirs.Select(s => "~/Views/" + s + "/{0}.cshtml")).ToArray();

			ViewLocationFormats =
                inputs.Select(s => "~/Views/" + s + "/{0}.cshtml").Concat(subdirs.Select(s => "~/Views/" + s + "/{0}.cshtml")).ToArray();
			;
		}
	}
}