using Game.Core.IO.Messages;

namespace Game.Core.IO
{
    public interface IMessageIO
    {
        /// <summary>
        /// Display a message to the user.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        void SendMessage(IMessage message);

        /// <summary>
        /// Ask a question and get an answer
        /// </summary>
        /// <param name="question">Question to ask</param>
        /// <returns>Answer to question; If <paramref name="question"/> has choices, the <see cref="Choice.Selector"/> of the selected <see cref="Choice"/> is returned</returns>
        string AskQuestion(IQuestion question);
    }
}
