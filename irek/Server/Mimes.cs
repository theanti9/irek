using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace irek.Server
{
	public static class Mimes
	{
		private static Hashtable Map;

		public static string Get(string ext)
		{
			Map = new Hashtable();
			Map.Add(".html", "text/html");
			Map.Add(".htm", "text/html");
			Map.Add(".css", "text/css");
			Map.Add(".js", "application/javascript");
			Map.Add(".jpg", "image/jpeg");
			Map.Add(".jpeg", "image/jpeg");
			Map.Add(".gif", "image/gif");
			Map.Add(".png", "image/png");
			Map.Add(".txt", "text/plain");
			Map.Add(".xml", "textl/xml");
			Map.Add(".swf", "application/x-shockwave-flash");
			Map.Add(".zip", "application/zip");
			Map.Add(".mp3", "audio/mpeg");
			Map.Add(".m3u", "audio/x-mpegurl");
			Map.Add(".wma", "audio/x-ms-wma");
			Map.Add(".wav", "audio/x-wav");
			Map.Add(".mpeg", "video/mpeg");
			Map.Add(".avi", "video/x-msvideo");
			Map.Add(".wmv", "video/x-ms-wmv");

			if (Map.ContainsKey(ext))
			{
				return (string)Map[ext];
			}
			else
			{
				return "text/plain";
			}
		}
	}
}
