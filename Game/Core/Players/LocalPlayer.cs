using Game.Core.Communication;
using Game.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public class LocalPlayer : PlayerBase
    {
        public override string AskQuestion(IQuestion question)
        {
            return GameConsole.AskQuestion(question);
        }

        public override void SendMessage(IMessage message)
        {
            GameConsole.DisplayMessage(message);
        }
    }
}
