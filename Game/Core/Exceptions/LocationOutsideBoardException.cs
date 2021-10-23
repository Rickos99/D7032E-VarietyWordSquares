using System;

namespace Game.Core.Exceptions
{
    class LocationOutsideBoardException : Exception
    {
        public LocationOutsideBoardException()
        {
        }

        public LocationOutsideBoardException(string message)
            : base(message)
        {
        }

        public LocationOutsideBoardException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
