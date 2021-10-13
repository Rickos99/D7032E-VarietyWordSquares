using System.Collections.Generic;

namespace Game.Core.Communication
{
    class PickALetterQuestion : IQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public string Content { get; private set; }

        public PickALetterQuestion(bool isScrabbleMode)
        {
            Choices = null;
            HasChoices = false;
            Content = string.Empty;
            if (isScrabbleMode)
            {
                Content += "LETTER POINTS\n"; // TODO Get from a dictionary
            }
            Content += "Pick a letter";
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
