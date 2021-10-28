using Game.Core.Communication;
using System;
using System.Linq;

namespace Game.Core.IO
{
    /// <summary>
    /// A console to use when there is a need for message transactions between two ends.
    /// </summary>
    public class GameConsole : IInputOutput
    {
        public void DisplayMessage(IMessage message)
        {
            Console.WriteLine(message.GetMessageString());
        }

        public string AskQuestion(IQuestion question)
        {
            DisplayMessage(question);
            if (question is ITimedQuestion q)
            {
                return TimedConsoleReader.ReadLine(q.SecondsTimeout * 1000);
            }
            if (question.HasChoices == false)
            {
                return GetInput();
            }

            return GetInput((input) => question.Choices.Any((c) => InputIsInChoices(c, input)));
        }

        public string GetInput(Predicate<string> validator)
        {
            string input = string.Empty;
            bool inputIsInvalid = true;
            while (inputIsInvalid)
            {
                input = Console.ReadLine();
                inputIsInvalid = !validator(input);
                if (inputIsInvalid)
                {
                    Console.WriteLine("Invalid input!");
                }
            }

            return input;
        }

        public string GetInput()
        {
            return GetInput(s => true);
        }

        private static bool InputIsInChoices(Choice choice, string input)
        {
            return choice.Selector.ToUpper() == input.ToUpper();
        }
    }
}
