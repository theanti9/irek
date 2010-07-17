using System;
using System.IO;
namespace irek
{
	public sealed class Logger {
   		private TextWriter access;
		private TextWriter error;
   		private static readonly Logger instance = new Logger();
   		private Logger() {
			access = new StreamWriter("access.log",true);
			error = new StreamWriter("error.log",true);
			TextWriter.Synchronized(access);
			TextWriter.Synchronized(error);
   		}
   		public static Logger GetInstance() {
      		return instance;
   		}
		public void LogAccess(string str) {
			access.WriteLine(str);
		}
   		public void LogError(string str) {
			Console.WriteLine(str);
      		error.WriteLine(str);
		}
		public void Kill() {
			access.Close();
			error.Close();
		}
	}
}

