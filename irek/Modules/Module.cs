using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using irek.Configuration;

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
            ModuleAssembly = Assembly.LoadFile(ModulePath);
        }
    }
}
