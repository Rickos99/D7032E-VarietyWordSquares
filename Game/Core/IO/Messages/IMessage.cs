namespace Game.Core.IO.Messages
{
    public interface IMessage
    {
        /// <summary>
        /// The message content
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Get message as a string.
        /// </summary>
        /// <returns>Message as a string</returns>
        string GetMessageString();
    }
}
