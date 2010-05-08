using System;

namespace irek
{
    class Program
    {
        public static irek.Server.Server srv;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting irek!");
            srv = new irek.Server.Server();
        }
    }
}
