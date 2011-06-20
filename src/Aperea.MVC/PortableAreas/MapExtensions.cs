// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
	public static class MapExtensions
	{
		public static T Title<T>(this T mapper, string title) where T : PortableAreaMap
		{
			mapper.Add(mapper.DefaultTitleID, title);
			return mapper;
		}

		public static T Body<T>(this T mapper, string body) where T : PortableAreaMap
		{
			mapper.Add(mapper.DefaultBodyID, body);
			return mapper;
		}

		public static T Master<T>(this T mapper, string master) where T : PortableAreaMap
		{
			mapper.MasterPageLocation = master;
			return mapper;
		}
	}
}
