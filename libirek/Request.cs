using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace libirek
{
    public class Request
    {
        public string RequestedPath;
        public Hashtable GET;
        public Hashtable POST;
        public Hashtable CLIENT;

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="fullrequest">The full request string.</param>
        public Request(string fullrequest, Regex rx)
        {
			GET = new Hashtable();
			POST = new Hashtable();
			CLIENT = new Hashtable();
            string line = fullrequest.Substring(0, fullrequest.IndexOf("\r\n"));
            RequestedPath = line.Substring(line.IndexOf(' ')+1, line.LastIndexOf(' ') - (line.IndexOf(' ')+1));
			Match match = rx.Match(RequestedPath);
			for (int i = 1; i < match.Groups.Count; i++)
			{
				GET.Add(rx.GroupNameFromNumber(i), match.Groups[i]);
			}
			if (fullrequest.Contains("Content-Length:"))
			{
				Regex rx2 = new Regex(@"\&*([a-zA-Z0-9_]+)=([^\&]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
				MatchCollection Matches = rx2.Matches(fullrequest.Substring(fullrequest.IndexOf("\r\n\r\n")+4));
				foreach (Match m in Matches)
				{
					GroupCollection groups = m.Groups;
					string val = groups[2].Value;
					if (val.StartsWith("&"))
					{
						val = val.Substring(1);
					}
					
					POST.Add(groups[1].Value, HttpUtility.UrlDecode(val));
				}
			}
        }
    }
}
