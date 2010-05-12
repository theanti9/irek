using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace libirek.Urls
{
    public class UrlMapItem
    {
        public string UrlPattern;
        public string Method;
		public string Path;
        public Regex rx;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlMapItem"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="method">The method.</param>
		/// <param name="path">The folder path (used only when mapping to a static content folder)</param>
        public UrlMapItem(string pattern, string method, string path = null)
        {
            UrlPattern = pattern;
            Method = method;
			Path = path;
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
