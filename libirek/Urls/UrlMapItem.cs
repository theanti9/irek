using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace libirek.Urls
{
    public class UrlMapItem
    {
        public string UrlPattern;
        public string Method;

        public Regex rx;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlMapItem"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="method">The method.</param>
        public UrlMapItem(string pattern, string method)
        {
            UrlPattern = pattern;
            Method = method;
            rx = new Regex(UrlPattern,RegexOptions.Compiled | RegexOptions.Singleline);
        }

        /// <summary>
        /// Determines whether the specified URL is match.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// 	<c>true</c> if the specified URL is match; otherwise, <c>false</c>.
        /// </returns>
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
