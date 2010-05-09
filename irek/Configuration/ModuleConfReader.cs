using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace irek.Configuration
{
    public class ModuleConfReader
    {
        public ArrayList Modules;
        public ArrayList Dependencies;
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleConfReader"/> class.
        /// </summary>
        /// <param name="filename">The filename of the Module Configuration file.</param>
        public ModuleConfReader(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new Errors.NoSuchConfigurationFileException("Error: Could not find modules.conf.");
            }
            Modules = new ArrayList();
            Dependencies = new ArrayList();
            StreamReader sr = File.OpenText(filename);
            string line;
            while((line = sr.ReadLine()) != null) {
                if (!line.StartsWith("//"))
                {
                    string[] parts = line.Split(" ".ToCharArray());
                    if (parts[0] == "mod")
                    {
                        Modules.Add(parts[1]);
                    }
                    else if (parts[0] == "dep")
                    {
                        Dependencies.Add(parts[1]);
                    }
                }
            }
        }
    }
}
