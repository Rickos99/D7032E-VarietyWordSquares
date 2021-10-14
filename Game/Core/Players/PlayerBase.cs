using Game.Core.Communication;
using System;

namespace Game.Core.Players
{
    // LocalPlayer, use Console to ask question and send message. ctor()
    // NetworkPlayer, use network interface to ask questions. ctor(TcpClient client)
    // BotPlayer, don't print anything, only get input. Need support for game ended message. ctor() or ctor(Obj[] answers)
    public abstract class PlayerBase
    {
        /// <summary>
        /// Player identifier
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="PlayerBase"/> class
        /// </summary>
        public PlayerBase()
        {    
            ID = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is not PlayerBase player) return false;

            return ID == player.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>
        /// Ask player a question and get the response
        /// </summary>
        /// <param name="question">Question to ask</param>
        /// <returns>Players response to question</returns>
        public abstract string AskQuestion(IQuestion question);

        /// <summary>
        /// Send a message to player
        /// </summary>
        /// <param name="message">Message to send</param>
        public abstract void SendMessage(IMessage message);
    }
}
