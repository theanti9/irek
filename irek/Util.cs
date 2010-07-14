using System;
using System.Text;

namespace irek
{
	public static class Util
	{
		public static string RandomString(int length)
		{
			char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
			StringBuilder sb = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				sb.Append(chars[i]);
			}
			return sb.ToString();
		}
	}
}