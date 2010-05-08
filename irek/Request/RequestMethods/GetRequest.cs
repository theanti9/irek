using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using irek;
using irek.Request;

namespace irek.Request.RequestMethods
{
    public class GetRequest
    {
        public string HttpVersion;
        public string QueryString;
        public string Path;
        public Header header;
        public GetRequest(string httpversion, string path, string querystring)
        {
            HttpVersion = httpversion;
            Path = path;
            QueryString = querystring;
        }

        public byte[] GetResponse()
        {
            string response;
            if (Directory.Exists(Path))
            {
                Path += "\\index.html";
            }
            if (!File.Exists(Path))
            {
                response = "<h1>Error: 404 File Not Found!</h1>";
                header = new Header(HttpVersion, "text/html", response.Length, " 404 Not Found");
            }
            else
            {
                int totalbytes = 0;
                StringBuilder responsesb = new StringBuilder("");
                byte[] bytes;

                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader reader = new BinaryReader(fs);
                bytes = new byte[fs.Length];
                int read;
                while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
                {
                    responsesb = responsesb.Append(Encoding.ASCII.GetString(bytes, 0, read));

                    totalbytes = totalbytes + read;
                }
                reader.Close();
                fs.Close();

                header = new Header(HttpVersion, "text/html", totalbytes, " 200 OK");
                response = responsesb.ToString();
            }
            string str = header.GetHeader() + response;
            byte[] bresponse = Encoding.ASCII.GetBytes(str);
            return bresponse;
        }
    }
}
