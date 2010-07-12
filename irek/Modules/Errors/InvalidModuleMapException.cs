using System;

namespace irek.Modules.Errors
{
    public class InvalidModuleMapException : System.Exception
    {
        public InvalidModuleMapException() : base() { }
        public InvalidModuleMapException(string message) : base(message) { }
    }
}
