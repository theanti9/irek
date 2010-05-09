using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irek.Modules.Errors
{
    class NoSuchModuleException : System.Exception
    {
        public NoSuchModuleException() : base() { }
        public NoSuchModuleException(string message) : base(message) { }
    }
}
