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

        public Request(string fullrequest)
        {
            string line = fullrequest.Substring(0, fullrequest.IndexOf("\r\n"));
            RequestedPath = line.Substring(line.IndexOf(' ')+1, line.LastIndexOf(' ') - (line.IndexOf(' ')+1));
        }
    }
}
