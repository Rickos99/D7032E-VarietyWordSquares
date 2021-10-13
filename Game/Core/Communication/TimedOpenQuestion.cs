using System;
using System.Collections.Generic;

namespace Game.Core.Communication
{
    public class TimedOpenQuestion : ITimedQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public int SecondsTimeout { get; private set; }

        public string Content { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedOpenQuestion"/> class.
        /// </summary>
        /// <param name="content">A question</param>
        /// <param name="secondsTimeout">Time to answer the question</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimedOpenQuestion(string content, int secondsTimeout)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException($"'{nameof(content)}' cannot be null or whitespace.", nameof(content));
            }

            if (secondsTimeout <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(secondsTimeout));
            }

            Choices = null;
            HasChoices = false;
            SecondsTimeout = secondsTimeout;
            Content = content;
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
