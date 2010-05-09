using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace irek.Request
{
    public class Request
    {
        public string RequestedPath;
        public Hashtable GET;
        public Hashtable CLIENT;

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="fullrequest">The full request string.</param>
        public Request(string fullrequest)
        {
            string line = fullrequest.Substring(0, fullrequest.IndexOf("\r\n"));
            RequestedPath = line.Substring(line.IndexOf(' ')+1, line.LastIndexOf(' ') - (line.IndexOf(' ')+1));
        }
    }
}
