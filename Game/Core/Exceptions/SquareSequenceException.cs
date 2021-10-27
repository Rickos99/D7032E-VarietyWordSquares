using System;

namespace Game.Core.Exceptions
{
    public class SquareSequenceFormatException : Exception
    {
        public SquareSequenceFormatException()
        {
        }

        public SquareSequenceFormatException(string message)
            : base(message)
        {
        }

        public SquareSequenceFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
