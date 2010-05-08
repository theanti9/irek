using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irek.Configuration.Errors
{
    class NoSuchConfigurationFileException : System.Exception
    {
        public NoSuchConfigurationFileException() : base() { }
        public NoSuchConfigurationFileException(string message) : base(message) { }
    }
}
