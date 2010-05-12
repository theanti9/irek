using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using irek.Configuration;
using irek.Modules.Errors;
using libirek.Urls;
namespace irek.Modules
{
    public class Module
    {
        public string ModulePath;
		public string ModuleNamespace;
        public Assembly ModuleAssembly;
        public List<UrlMapItem> UrlMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        /// <param name="modulepath">The modulepath.</param>
        public Module(string modulepath)
        {
            ModulePath = modulepath;
        }

        /// <summary>
        /// Loads this the module and instantiates the UrlMap.
        /// </summary>
        public void Load()
        {
            if (!File.Exists(ModulePath))
            {
                throw new NoSuchModuleException("Error: No such module at '" + ModulePath + "'.");
            }
            ModuleAssembly = Assembly.LoadFile(ModulePath);
            int lastslash = ModulePath.LastIndexOf(@"\");
            string assemblynamespace = ModulePath.Substring(lastslash+1, ModulePath.LastIndexOf('.') - lastslash-1);
			ModuleNamespace = assemblynamespace;
            Type t = ModuleAssembly.GetType(assemblynamespace+".ModuleMap");
            if (t != null)
            {
                MethodInfo m = t.GetMethod("GetUrlMap");
                if (m != null)
                {
                    UrlMap = (List<UrlMapItem>)m.Invoke(null, (new object[]{}));
                }
                else
                {
                    throw new InvalidModuleMapException("Error: The ModuleMap class is incorrect!");
                }
            }
            else
            {
                throw new InvalidModuleMapException("Error: The ModuleMap class is missing!");
            }
        }
    }
}
