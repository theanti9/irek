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
        public readonly string RequestedPath;
		public readonly bool HasSessionId;
		public readonly string SessionId;
        public readonly Hashtable GET;
        public readonly Hashtable POST;
        public readonly Hashtable CLIENT;
		public Hashtable COOKIE;
		public Hashtable SESSION;

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="fullrequest">The full request string.</param>
        public Request(string fullrequest, Regex rx)
        {
			HasSessionId = false;
			GET = new Hashtable();
			POST = new Hashtable();
			CLIENT = new Hashtable();
			COOKIE = new Hashtable();
			SESSION = new Hashtable();
            string line = fullrequest.Substring(0, fullrequest.IndexOf("\r\n"));
			int firstspace = line.IndexOf(' ') + 1;
            RequestedPath = line.Substring(firstspace, line.LastIndexOf(' ') - firstspace);
			Match match = rx.Match(RequestedPath);
			for (int i = 1; i < match.Groups.Count; i++)
			{
				GET.Add(rx.GroupNameFromNumber(i), HttpUtility.UrlDecode(match.Groups[i].Value));
			}
			Regex sessidrx = new Regex(@"ISESSION=([A-Z0-9]{16})", RegexOptions.Compiled);
			if (sessidrx.IsMatch(fullrequest))
			{
				HasSessionId = true;
				Match m = sessidrx.Match(fullrequest);
				GroupCollection g = m.Groups;
				SessionId = g[1].Value;
			}
			if (line.Split(new char[] { ' ' })[0] == "POST")
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
