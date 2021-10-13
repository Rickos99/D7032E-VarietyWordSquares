using System;
using System.Runtime.Serialization;

namespace Game.Core.Exceptions
{
    public class UnknownMessageException : Exception
    {
        public UnknownMessageException()
        {
        }

        public UnknownMessageException(string message)
            : base(message)
        {
        }

        public UnknownMessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnknownMessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
