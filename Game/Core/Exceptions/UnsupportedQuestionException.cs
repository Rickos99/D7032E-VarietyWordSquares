using System;

namespace Game.Core.Exceptions
{
    class UnsupportedQuestionException : Exception
    {
        public UnsupportedQuestionException(Type questionRef)
            : base(GetExceptionMessage(questionRef))
        {
        }

        public UnsupportedQuestionException(Type questionRef, string message)
            : base(GetExceptionMessage(questionRef, message))
        {
        }

        public UnsupportedQuestionException(Type questionRef, string message, Exception innerException)
            : base(GetExceptionMessage(questionRef, message), innerException)
        {
        }

        private static string GetExceptionMessage(Type questionRef, string message = "")
        {
            return $"Question of type '{questionRef.GetType().Name}' is not suppported. {message}";
        }
    }
}
