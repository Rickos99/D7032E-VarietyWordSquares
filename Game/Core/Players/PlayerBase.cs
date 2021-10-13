using Game.Core.Communication;
using System;

namespace Game.Core.Players
{
    // LocalPlayer, use Console to ask question and send message. ctor()
    // NetworkPlayer, use network interface to ask questions. ctor(TcpClient client)
    // BotPlayer, don't print anything, only get input. Need support for game ended message. ctor() or ctor(Obj[] answers)
    public abstract class PlayerBase
    {
        public string ID { get; private set; }

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

        public abstract string AskQuestion(IQuestion question);

        public abstract void SendMessage(IMessage message);
    }
}
