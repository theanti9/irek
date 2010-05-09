using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irek.Modules.Errors
{
    public class MissingDependencyException : System.Exception
    {
        public MissingDependencyException() : base() { }
        public MissingDependencyException(string message) : base(message) { }
    }
}
