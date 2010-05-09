using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace libirek
{
    public class MethodCall
    {
        public string MethodNamespace;
        public string MethodName;
        public Hashtable Data;

        public MethodCall(string methodstring, Regex rx, string url)
        {
            MethodNamespace = methodstring.Substring(0, methodstring.LastIndexOf('.'));
            MethodName = methodstring.Substring(methodstring.LastIndexOf('.') + 1);
            Match datamatch = rx.Match(url);
            Data = new Hashtable();
            for (int i = 1; i < datamatch.Groups.Count; i++)
            {
                Data.Add(rx.GroupNameFromNumber(i), datamatch.Groups[i]);
            }
        }
    }
}
