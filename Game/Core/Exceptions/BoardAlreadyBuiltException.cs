using System;

namespace Game.Core.Exceptions
{
    class BoardAlreadyBuiltException : Exception
    {
        public BoardAlreadyBuiltException()
        {
        }

        public BoardAlreadyBuiltException(string message)
            : base(message)
        {
        }

        public BoardAlreadyBuiltException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
