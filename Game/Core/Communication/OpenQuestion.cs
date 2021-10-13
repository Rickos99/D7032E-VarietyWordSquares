using System;
using System.Collections.Generic;

namespace Game.Core.Communication
{
    public class OpenQuestion : IQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public string Content { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenQuestion"/> class.
        /// </summary>
        /// <param name="content">A question</param>
        /// <exception cref="ArgumentException"></exception>
        public OpenQuestion(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException($"'{nameof(content)}' cannot be null or whitespace.", nameof(content));
            }

            Choices = null;
            HasChoices = false;
            Content = content;
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
