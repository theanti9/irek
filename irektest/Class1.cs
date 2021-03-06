﻿using System;
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
			map.Add(new UrlMapItem(@"^\/goodbye\/", "irektest.Class1.goodbye"));
			map.Add(new UrlMapItem(@"^\/media\/(.+)", "irek.static", @"E:\"));

			return map;
		}
	}
	public class Class1
	{
		public static Page hello(Request rq)
		{
			string body = "<form method='post'><input type='text' name='name' /><input type='password' name='pass' /><input type='submit' value='Submit' /></form><br />";
			if (rq.POST.Count > 0)
			{
				body += "<p>Name: " + rq.POST["name"] + "</p>";
				body += "<p>Password: " + rq.POST["pass"] + "</p>";
			}
			return (new Page(body));
		}
		public static Page goodbye(Request rq)
		{
			return (new Page(rq.RequestedPath));
		}
	}
}
