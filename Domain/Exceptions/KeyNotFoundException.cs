using System;
namespace Domain.Exceptions
{
	public class KeyNotFoundException : Exception
	{
        public KeyNotFoundException()
        {
        }

        public KeyNotFoundException(string message) : base(message)
        {
        }

        public KeyNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

