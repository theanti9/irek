using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libirek;
using libirek.Urls;
namespace irektest
{
	public static class ModuleMap
	{
		public static List<UrlMapItem> GetUrlMap()
		{
			List<UrlMapItem> map = new List<UrlMapItem>();
			map.Add(new UrlMapItem(@"^\/hello\/", "irektest.Class1.hello"));

			return map;
		}
	}
	public class Class1
	{
		public static Page hello()
		{
			return (new Page("<p>Hello, world!</p>"));
		}
	}
}
