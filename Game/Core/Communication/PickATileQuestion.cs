using System.Collections.Generic;

namespace Game.Core.Communication
{
    public class PickATileQuestion : IQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public string Content { get; private set; }

        public PickATileQuestion()
        {
            Choices = null;
            HasChoices = false;
            Content += "Pick a letter";
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
