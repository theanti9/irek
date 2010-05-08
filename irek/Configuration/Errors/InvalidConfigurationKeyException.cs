using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irek.Configuration.Errors
{
    class InvalidConfigurationKeyException : System.Exception
    {
        public InvalidConfigurationKeyException() : base() { }
        public InvalidConfigurationKeyException(string message) : base(message) { }
    }
}
