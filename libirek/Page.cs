using System;
using System.Collections.Generic;
using System.Text;

namespace libirek
{
	public class Page
	{
		private Header PageHeader;
		private string Body;

		public Page(string body)
		{
			PageHeader = new Header("HTTP/1.1", "text/html", body.Length, " 200 OK");
			Body = body;
		}

		public Header GetHeader() {
			return PageHeader;
		}

		public string GetBody()
		{
			return Body;
		}

	}
}
