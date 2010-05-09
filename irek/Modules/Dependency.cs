using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using irek.Modules.Errors;
namespace irek.Modules
{
    public class Dependency
    {
        public string DependencyPath;
        public Assembly DependencyAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dependency"/> class.
        /// </summary>
        /// <param name="deppath">The dependency path.</param>
        public Dependency(string deppath)
        {
            DependencyPath = deppath;
        }

        public void Load()
        {
            if (!File.Exists(DependencyPath))
            {
                throw new MissingDependencyException("Error: Missing application dependency. No such file at '" + DependencyPath + "'.");
            }

            DependencyAssembly = Assembly.LoadFile(DependencyPath);
        }
    }
}
