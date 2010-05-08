using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irek.Request
{
    public class Header
    {
        string header;
        public Header(string HttpVersion, string ContentType, int ContentLength, string Status)
        {
            String buffer = "";

            if (ContentType.Length == 0)
            {
                ContentType = "text/plain";
            }
            buffer = buffer + HttpVersion + Status + "\r\n";
            buffer = buffer + "Server: irek\r\n";
            buffer = buffer + "Content-Type: " + ContentType + "\r\n";
            buffer = buffer + "Accept-Ranges: bytes\r\n";
            buffer = buffer + "Content-Length: " + ContentLength + "\r\n\r\n";
            header = buffer;
        }

        public string GetHeader()
        {
            return header;
        }
    }
}
