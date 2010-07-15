using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using irek.Configuration;
using libirek;
using libirek.Urls;
namespace irek.Server
{
    public static class RequestHandler
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="config">The config.</param>
		/// <param name="UrlMap">The Url Map list</param>
		/// <param name="ModuleList">The ModuleList hashtable</param>
        /// <returns>byte[] server response</returns>
        public static byte[] Handle(string request, ref Config config, ref List<UrlMapItem> UrlMap, ref Hashtable ModuleList)
        {
			string firstline = request.Split((new string[] { "\r\n" }),StringSplitOptions.None)[0];
			StringBuilder logline = new StringBuilder();
			logline.Append(firstline);
			int space = firstline.IndexOf(' ');
			string path = firstline.Substring(space + 1, firstline.LastIndexOf(' ')-space-1);
			string method = null;
			string staticpath = null;
			Regex rx = null;
			foreach (UrlMapItem item in UrlMap) {
				if (item.IsMatch(path))
				{
					method = item.Method;
					rx = item.rx;
					staticpath = item.Path;
				}
			}
			logline.Append(" : " + method);
			if (!String.IsNullOrEmpty(staticpath)) {
				logline.Append(" " + staticpath);
			}
			if (string.IsNullOrEmpty(method))
			{
				logline.Append(" : 404");
				Logger.GetInstance().LogAccess(logline.ToString());
				return Encoding.ASCII.GetBytes(Get404());
				
			}
			if (method == "irek.static") {
				Match match = rx.Match(path);
				string addpath = match.Groups[1].Value;
				if (!staticpath.EndsWith(@"\"))
				{
					staticpath += "\\";
				}
#if (Windows && !Unix)
				staticpath = staticpath + addpath.Replace("/", "\\");
#else
				staticpath = staticpath + addpath;
#endif
				if (File.Exists(staticpath))
				{
					int numbytes = (int)new FileInfo(staticpath).Length;
					BinaryReader br = new BinaryReader(new FileStream(staticpath, FileMode.Open, FileAccess.Read));
					string contenttype = Mimes.Get(staticpath.Substring(staticpath.LastIndexOf(".")));
					string head = (new Header("HTTP/1.1", contenttype, numbytes, " 200 OK")).GetHeader();
					int len = numbytes + head.Length;
					byte[] buff = new byte[len];
					Buffer.BlockCopy(Encoding.ASCII.GetBytes(head), 0, buff, 0, head.Length);
					Buffer.BlockCopy(br.ReadBytes(numbytes), 0, buff, head.Length, numbytes);
					logline.Append(" : 200");
					Logger.GetInstance().LogAccess(logline.ToString());
					return buff;
				}
				else
				{
					logline.Append(" : 404");
					Logger.GetInstance().LogAccess(logline.ToString());
					return Encoding.ASCII.GetBytes(Get404());
				}
			}
			Request rq = new Request(request, rx);
			string methodnamespace = method.Substring(0, method.IndexOf('.'));
			try
			{
				Assembly assembly = (Assembly)ModuleList[methodnamespace];
				Type t = assembly.GetType(method.Substring(0, method.LastIndexOf('.')));
				MethodInfo m = t.GetMethod(method.Substring(method.LastIndexOf('.') + 1));
				Page p = (Page)m.Invoke(null, (new object[] { rq }));
				p.PageHeader.SetCookie("ISESSION=" + Util.RandomString(16) + ";path=/");
				return Encoding.ASCII.GetBytes(p.GetHeader() + p.GetBody());
			}
			catch (Exception e)
			{
				Logger.GetInstance().LogError(e.Message + " From " + e.Source);
				return Encoding.ASCII.GetBytes(Get404());
			}
		}

		public static string Get404()
		{
			string b = "<h1>Error 404: Page Not Found!</h1>";
			Header h = new Header("HTTP/1.1", "text/html", b.Length, " 404 Not Found");
			return (h.GetHeader() + b);
		}
    }
}
