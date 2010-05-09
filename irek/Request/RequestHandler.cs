using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using irek.Configuration;
using irek.Request.RequestMethods;

namespace irek.Request
{
    public static class RequestHandler
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public static byte[] Handle(string request, ref Config config)
        {
            string HttpVersion;
            string Path;
            string QueryString;
            request = request.Substring(0, request.IndexOf("\r\n"));
            string method = request.Substring(0, request.IndexOf(' '));
            string P = "";
            HttpVersion = request.Substring(request.LastIndexOf(' ') + 1);
            int q = request.IndexOf('?');
            if (q > -1)
            {
                QueryString = request.Substring(q + 1, request.LastIndexOf(' ') - q);
            }
            else
            {
                QueryString = "";
            }
            P = request.Substring(request.IndexOf(' '), request.LastIndexOf(' ') - request.IndexOf(' '));
            P.Replace("%20", " ");
            P = P.Substring(1);

            Path = config.Get("homedir") + "\\" + P;

            byte[] response = null;

            if (method == "GET")
            {
                GetRequest rq = new GetRequest(HttpVersion, Path, QueryString);
                response = rq.GetResponse();
            }
            else if (method == "POST")
            {

            }
            else if (method == "HEAD")
            {

            }
            else
            {

            }

            return response;
        }
    }
}
