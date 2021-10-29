using Game.Core.IO.Messages;
using System;

namespace Game.Core.IO
{
    public interface IInputOutput
    {
        /// <summary>
        /// Display a message to the user.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        void DisplayMessage(IMessage message);

        /// <summary>
        /// Ask a question and get an answer
        /// </summary>
        /// <param name="question">Question to ask</param>
        /// <returns>Answer to question; If <paramref name="question"/> has choices, the <see cref="Choice.Selector"/> of the selected <see cref="Choice"/> is returned</returns>
        string AskQuestion(IQuestion question);

        /// <summary>
        /// Get text input from user.
        /// </summary>
        /// <returns>Text entered by user</returns>
        string GetInput();

        /// <summary>
        /// Get text input from user and ensure the text fulfills the requirements specified in validator.
        /// </summary>
        /// <param name="validator">Validator to use to validate the user text input</param>
        /// <returns>The text input from user that has passed the validator</returns>
        string GetInput(Predicate<string> validator);
    }
}
