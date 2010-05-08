using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace libirek.Urls
{
    public class UrlMapItem
    {
        public string UrlPattern;
        public string Method;

        private Regex rx;

        public UrlMapItem(string pattern, string method)
        {
            UrlPattern = pattern;
            Method = method;
            rx = new Regex(UrlPattern,RegexOptions.Compiled | RegexOptions.Singleline);
        }

        public bool IsMatch(string url)
        {
            if (rx.IsMatch(url))
            {
                return true;
            }
            return false;
        }
    }
}
