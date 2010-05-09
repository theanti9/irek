using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using irek.Configuration;
using irek.Configuration.Errors;
using irek.Modules.Errors;
using libirek.Urls;
namespace irek.Modules
{
    public class Module
    {
        public string ModulePath;
        public Assembly ModuleAssembly;
        public UrlMap<UrlMapItem> UrlMap;

        public Module(string modulepath)
        {
            ModulePath = modulepath;
        }

        public void Load()
        {
            if (!File.Exists(ModulePath))
            {
                throw new NoSuchModuleException("Error: No such module at '" + ModulePath + "'.");
            }
            ModuleAssembly = Assembly.LoadFile(ModulePath);
            int lastslash = ModulePath.LastIndexOf(@"\");
            string assemblynamespace = ModulePath.Substring(lastslash, ModulePath.LastIndexOf('.') - lastslash);
            Type t = ModuleAssembly.GetType(assemblynamespace+".ModuleMap");
            if (t != null)
            {
                MethodInfo m = t.GetMethod("GetUrlMap");
                if (m != null)
                {
                    object[] mparams = new object[0];
                    UrlMap = (UrlMap<UrlMapItem>)m.Invoke(null, mparams);
                }
            }
            else
            {
                throw new InvalidModuleMapException("Error: The ModuleMap class is missing or incorrect!");
            }
        }
    }
}
