using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using irek.Configuration;
using irek.Configuration.Errors

namespace irek.Modules
{
    public class Module
    {
        public string ModulePath;
        public Assembly ModuleAssembly;

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
        }
    }
}
