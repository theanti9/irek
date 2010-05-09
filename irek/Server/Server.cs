using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using irek.Configuration;
using irek.Configuration.Errors;
namespace irek.Server
{
    public class Server
    {
        public Config ServerConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server()
        {
            Console.WriteLine("Loading Configuration...");
            try
            {
                ServerConfig = new Config(Directory.GetCurrentDirectory() + "\\irek.conf");
            }
            catch (NoSuchConfigurationFileException e)
            {
                Console.WriteLine("Error: " + e.Message);
                Environment.Exit(1);
            }
            Console.WriteLine("Initializing...");
            Listener listener = new Listener(ServerConfig);
            //Thread t = new Thread(new ThreadStart(listener.Run));
            listener.Run();

        }
    }
}
