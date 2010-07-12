using System;
using System.Collections.Generic;
using System.Text;

namespace libirek
{
	public class Page
	{
		public Header PageHeader;
		public string Body;

		public Page(string body)
		{
			PageHeader = new Header("HTTP/1.1", "text/html", body.Length, " 200 OK");
			Body = body;
		}

		public string GetHeader() {
			return PageHeader.GetHeader();
		}

		public string GetBody()
		{
			return Body;
		}

		public void SetContentType(string contenttype)
		{
			PageHeader.SetContentType(contenttype);
		}
	}
}
