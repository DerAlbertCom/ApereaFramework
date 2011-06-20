using System;
using System.Collections.Generic;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
	public class PortableAreaContent
	{
		private static PortableAreaMap _defaultMap;

		private static Dictionary<Type, PortableAreaMap> _maps;

		public static Dictionary<Type, PortableAreaMap> Maps
		{
			get
			{
				if (_maps == null)
				{
					_maps = new Dictionary<Type, PortableAreaMap>();
				}
				return _maps;
			}
			set
			{
				_maps = value;
			}
		}

		public static PortableAreaMap MapAll()
		{
			_defaultMap = new PortableAreaMap();
			return _defaultMap;
		}

		public static T Map<T>() where T : PortableAreaMap
		{
			PortableAreaMap map = null;

			if (!Maps.TryGetValue(typeof(T), out map))
			{
				map = CreateMapWithDefault<T>(map);
				Maps.Add(typeof(T), map);
			}

			return map as T;
		}

		private static PortableAreaMap CreateMapWithDefault<T>(PortableAreaMap map) where T : PortableAreaMap
		{
			map = Activator.CreateInstance<T>();

			if (_defaultMap != null)
			{
				map.Master(_defaultMap.MasterPageLocation)
					.Title(_defaultMap.Title)
					.Body(_defaultMap.Body);
			}

			return map;
		}
	}
}
