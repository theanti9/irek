using System;

namespace irek
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting irek!");
            irek.Server.Server srv = new irek.Server.Server();
        }
    }
}
