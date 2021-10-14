using System;

namespace Game.Core.Exceptions
{
    public class SquareAlreadyOccupiedException : Exception
    {
        public SquareAlreadyOccupiedException()
        {
        }

        public SquareAlreadyOccupiedException(string message) : base(message)
        {
        }

        public SquareAlreadyOccupiedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
