using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using irek.Configuration;
using libirek;
using libirek.Urls;
namespace irek
{
    public static class RequestHandler
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public static byte[] Handle(string request, ref Config config, ref List<UrlMapItem> UrlMap, ref Hashtable ModuleList)
        {
			string firstline = request.Split((new string[] { "\r\n" }),StringSplitOptions.None)[0];
			int space = firstline.IndexOf(' ');
			string path = firstline.Substring(space + 1, firstline.LastIndexOf(' ')-space-1);
			string method = null;
			Regex rx = null;
			foreach (UrlMapItem item in UrlMap) {
				if (item.IsMatch(path))
				{
					method = item.Method;
					rx = item.rx;
				}
			}
			Request rq = new Request(request, rx);
			string methodnamespace = method.Substring(0, method.IndexOf('.'));
			Assembly assembly = (Assembly)ModuleList[methodnamespace];
			Type t = assembly.GetType(method.Substring(0, method.LastIndexOf('.')));
			MethodInfo m = t.GetMethod(method.Substring(method.LastIndexOf('.') + 1));
			Page p = (Page)m.Invoke(null, (new object[] { rq }));
			return Encoding.ASCII.GetBytes(p.GetHeader().GetHeader() + p.GetBody());

		}
    }
}
