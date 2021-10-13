using Game.Core.Communication;
using Game.Core.IO;

namespace Game.Core.Players
{
    public class LocalPlayer : PlayerBase
    {
        private IInputOutput _gameConsole;

        public LocalPlayer()
        {
            _gameConsole = new GameConsole();
        }

        public override string AskQuestion(IQuestion question)
        {
            return _gameConsole.AskQuestion(question);
        }

        public override void SendMessage(IMessage message)
        {
            _gameConsole.DisplayMessage(message);
        }
    }
}
