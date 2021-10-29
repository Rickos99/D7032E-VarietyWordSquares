using System;

namespace Game.Core.IO.Messages
{
    public class Choice
    {
        public string Selector { get; private set; }

        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Choice"/> class with a selector
        /// and a description of the choice.
        /// </summary>
        /// <param name="selector">A string in which the choice can be identified by</param>
        /// <param name="description">A description of the choice</param>
        /// <exception cref="ArgumentException"></exception>
        public Choice(string selector, string description)
        {
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new ArgumentException($"'{nameof(selector)}' cannot be null or whitespace.", nameof(selector));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException($"'{nameof(description)}' cannot be null or whitespace.", nameof(description));
            }

            Selector = selector;
            Description = description;
        }
    }
}
