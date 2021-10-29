using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Core.IO.Messages
{
    public class ClosedQuestion : IQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public string Content { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClosedQuestion"/> class with
        /// a question and some choices.
        /// </summary>
        /// <param name="content">A question</param>
        /// <param name="choices">A list of choices when answering the question</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public ClosedQuestion(string content, IList<Choice> choices)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException($"'{nameof(content)}' cannot be null or whitespace.", nameof(content));
            }

            if (choices is null)
            {
                throw new ArgumentNullException(nameof(choices));
            }

            Choices = choices;
            HasChoices = true;
            Content = content;
        }

        public string GetMessageString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Content);
            foreach (var choice in Choices)
            {
                sb.AppendLine($"  [{choice.Selector}] {choice.Description}");
            }

            // Remove new line after last choice
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
    }
}
