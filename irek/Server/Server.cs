//#define Windows
#define Unix
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using irek.Configuration;
using irek.Configuration.Errors;
using irek.Modules;
using irek.Modules.Errors;
using libirek.Urls;

namespace irek.Server
{
    public class Server
    {
        public Config ServerConfig;
        public ModuleConfReader ModuleConfig;
        public List<UrlMapItem> GlobalUrlMap;
		public Hashtable ModuleList;
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server()
        {
            Console.WriteLine("Loading Configuration...");
            try
            {
#if (Windows && !Unix)
				ServerConfig = new Config(Directory.GetCurrentDirectory() + "\\irek.conf");
#else
               	ServerConfig = new Config(Directory.GetCurrentDirectory() + "/irek.conf");
#endif
            }
            catch (NoSuchConfigurationFileException e)
            {
                Console.WriteLine("Error: " + e.Message);
                Environment.Exit(1);
            }
			
#if (Windows && !Unix)
			ModuleConfig = new ModuleConfReader(Directory.GetCurrentDirectory() + "\\modules.conf");
#else
			ModuleConfig = new ModuleConfReader(Directory.GetCurrentDirectory() + "/modules.conf");
#endif
            foreach (string depstring in ModuleConfig.Dependencies)
            {
                Dependency dep = new Dependency(depstring);
                dep.Load();
            }

            GlobalUrlMap = new List<UrlMapItem>();
			ModuleList = new Hashtable();
            foreach (string modstring in ModuleConfig.Modules)
            {
                Module mod = new Module(modstring);
                mod.Load();
				ModuleList.Add(mod.ModuleNamespace, mod.ModuleAssembly);
				List<UrlMapItem> tempmap = mod.UrlMap;
                foreach (UrlMapItem mapitem in tempmap)
                {
					foreach (UrlMapItem item in GlobalUrlMap) {
						if (item.UrlPattern == mapitem.UrlPattern) {
							throw new DuplicateUrlPatternException("Error: Same url pattern used in multiple places");
						}
					}
                    GlobalUrlMap.Add(mapitem);
                }
            }
            
            Console.WriteLine("Initializing...");
            Listener listener = new Listener(ServerConfig, GlobalUrlMap, ModuleList);
            listener.Run();
        }
    }
}
