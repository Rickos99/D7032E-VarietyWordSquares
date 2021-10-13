using System.Collections.Generic;

namespace Game.Core.Communication
{
    public interface IQuestion : IMessage
    {
        /// <summary>
        /// List of all available choices.
        /// </summary>
        IList<Choice> Choices { get; }

        /// <summary>
        /// Indicates whether the message has any choices.
        /// </summary>
        bool HasChoices { get; }
    }
}
