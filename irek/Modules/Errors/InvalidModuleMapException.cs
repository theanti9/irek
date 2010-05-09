using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irek.Modules.Errors
{
    public class InvalidModuleMapException : System.Exception
    {
        public InvalidModuleMapException() : base() { }
        public InvalidModuleMapException(string message) : base(message) { }
    }
}
