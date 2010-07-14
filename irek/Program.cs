using System;

namespace irek
{
    class Program
    {
        public static irek.Server.Server srv;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting irek!");
			try {
            	srv = new irek.Server.Server();
			} catch (Exception e) {
				Logger.GetInstance().LogError(e.Message + " From " + e.Source);
				Environment.Exit(1);
			}
        }
    }
}
