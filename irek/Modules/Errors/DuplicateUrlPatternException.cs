using System;
namespace irek
{
	public class DuplicateUrlPatternException : System.Exception
	{
		public DuplicateUrlPatternException() : base() { }
		public DuplicateUrlPatternException(string message) : base(message) { }
	}
}

