using Game.Core.Communication;
using System;
using System.Linq;

namespace Game.Core.IO
{
    class GameConsole //: IInput, IOutput
    {
        public static void DisplayMessage(IMessage message)
        {
            Console.WriteLine(message.GetMessageString());
        }

        public static string AskQuestion(IQuestion question)
        {
            DisplayMessage(question);
            if (question.HasChoices == false) return GetInput();

            return GetInput((input) => question.Choices.Any((c) => InputIsInChoices(c, input)));
        }

        public static string GetInput(Predicate<string> validator)
        {
            string input;
            do
            {
                input = Console.ReadLine();
            } while (!validator(input));

            return input;
        }

        public static string GetInput()
        {
            return GetInput(s => true);
        }

        private static bool InputIsInChoices(Choice choice, string input)
        {
            return choice.Selector.ToUpper() == input.ToUpper();
        }
    }
}
