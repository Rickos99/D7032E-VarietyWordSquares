using System;
using System.Collections.Generic;

namespace Game.Core.Communication
{
    public class InformationMessage : IMessage
    {
        public string Content { get; private set; }

        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationMessage"/> class with 
        /// a specified message.
        /// </summary>
        /// <param name="content">The message</param>
        /// <exception cref="ArgumentException"></exception>
        public InformationMessage(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException($"'{nameof(content)}' cannot be null or whitespace.", nameof(content));
            }

            Content = content;
            Choices = null;
            HasChoices = false;
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
