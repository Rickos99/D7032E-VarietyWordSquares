using System.Collections.Generic;

namespace Game.Core.Communication
{
    class PickLetterLocationQuestion : IQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public string Content { get; private set; }

        public PickLetterLocationQuestion(char letter)
        {
            Choices = null;
            HasChoices = false;
            Content = $"Place {letter} (syntax: row column)";
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
