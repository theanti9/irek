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
			if (method == "irek.static") {
				Match match = rx.Match(path);
				string addpath = match.Groups[1].Value;
				if (!staticpath.EndsWith(@"\"))
				{
					staticpath += "\\";
				}
				staticpath = staticpath + addpath.Replace("/", "\\");
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
					return buff;
				}
				else
				{
					return null;
				}
			}
			Request rq = new Request(request, rx);
			string methodnamespace = method.Substring(0, method.IndexOf('.'));
			Assembly assembly = (Assembly)ModuleList[methodnamespace];
			Type t = assembly.GetType(method.Substring(0, method.LastIndexOf('.')));
			MethodInfo m = t.GetMethod(method.Substring(method.LastIndexOf('.') + 1));
			Page p = (Page)m.Invoke(null, (new object[] { rq }));
			return Encoding.ASCII.GetBytes(p.GetHeader() + p.GetBody());
		}
    }
}
