using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libirek
{
    public class Header
    {
        string header;
		string ContentType;
		string HttpVersion;
		int ContentLength;
		string HttpStatus;
		string Cookie;
        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="HttpVersion">The HTTP version.</param>
        /// <param name="ContentType">Type of the content.</param>
        /// <param name="ContentLength">Length of the content.</param>
        /// <param name="Status">The status.</param>
        public Header(string Version, string Type, int Length, string Status)
        {
			HttpVersion = Version;
			ContentType = Type;
			HttpStatus = Status;
			ContentLength = Length;
			Cookie = null;
            String buffer = "";

            if (ContentType.Length == 0)
            {
                ContentType = "text/html";
            }
            buffer = buffer + HttpVersion + HttpStatus + "\r\n";
            buffer = buffer + "Server: irek\r\n";
            buffer = buffer + "Content-Type: " + ContentType + "\r\n";
            buffer = buffer + "Accept-Ranges: bytes\r\n";
            buffer = buffer + "Content-Length: " + ContentLength + "\r\n\r\n";
            header = buffer;
        }


		/// <summary>
		/// Changes the content type from the default of text/html to newcontentype
		/// </summary>
		/// <param name="newcontenttype">What to set the content type to</param>
		public void SetContentType(string newcontenttype)
		{
			ContentType = newcontenttype;
		}

		/// <summary>
		///  Sets the content length of the headers
		/// </summary>
		/// <param name="length">The new content length</param>
		public void SetContentLenght(int length)
		{
			ContentLength = length;
		}

		/// <summary>
		/// Set ext
		/// </summary>
		/// <param name="cookie"></param>
		public void SetCookie(string cookie)
		{
			Cookie += "Set-Cookie: " + cookie;
		}

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <returns>String</returns>
        public string GetHeader()
        {
			string buffer = "";
			buffer = buffer + HttpVersion + HttpStatus + "\r\n";
			buffer = buffer + "Server: irek\r\n";
			buffer = buffer + "Content-Type: " + ContentType + "\r\n";
			buffer = buffer + "Accept-Ranges: bytes\r\n";
			buffer = buffer + "Content-Length: " + ContentLength + "\r\n";
			if (!string.IsNullOrEmpty(Cookie))
			{
				buffer = buffer + Cookie + "\r\n";
			}
			buffer = buffer + "\r\n";
			header = buffer;
            return header;
        }
    }
}
