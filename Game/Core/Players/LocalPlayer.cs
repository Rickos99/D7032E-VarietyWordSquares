using Game.Core.Communication;
using Game.Core.IO;

namespace Game.Core.Players
{
    /// <summary>
    /// A human player located on the local machine.
    /// </summary>
    public class LocalPlayer : PlayerBase
    {
        private readonly IInputOutput _inputOutput;

        public LocalPlayer(IInputOutput inputOutput)
        {
            _inputOutput = inputOutput;
        }

        public override string AskQuestion(IQuestion question)
        {
            return _inputOutput.AskQuestion(question);
        }

        public override void SendMessage(IMessage message)
        {
            _inputOutput.DisplayMessage(message);
        }
    }
}
