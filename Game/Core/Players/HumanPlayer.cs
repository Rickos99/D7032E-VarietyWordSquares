using Game.Core.IO;
using Game.Core.IO.Messages;

namespace Game.Core.Players
{
    /// <summary>
    /// A human player located on the local machine.
    /// </summary>
    public class HumanPlayer : PlayerBase
    {
        private readonly IMessageIO _inputOutput;

        public HumanPlayer(IMessageIO inputOutput)
        {
            _inputOutput = inputOutput;
        }

        public override string AskQuestion(IQuestion question)
        {
            return _inputOutput.AskQuestion(question);
        }

        public override void SendMessage(IMessage message)
        {
            _inputOutput.SendMessage(message);
        }
    }
}
