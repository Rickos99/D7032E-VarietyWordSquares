using Game.Core.Communication;

namespace Game.Core.IO
{
    public interface IOutput
    {
        /// <summary>
        /// Display a message to the user.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        void DisplayMessage(IMessage message);
    }
}
