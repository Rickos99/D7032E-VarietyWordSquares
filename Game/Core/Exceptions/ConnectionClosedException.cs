using System;

namespace Game.Core.Exceptions
{
    class ConnectionClosedException : Exception
    {
        public ConnectionClosedException()
        {
        }

        public ConnectionClosedException(string message)
            : base(message)
        {
        }

        public ConnectionClosedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
