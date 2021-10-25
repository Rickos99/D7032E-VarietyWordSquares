using System;

namespace Game.Core.Communication
{
    /// <summary>
    /// Simple text message
    /// </summary>
    public class InformationMessage : IMessage
    {
        public string Content { get; private set; }

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
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
