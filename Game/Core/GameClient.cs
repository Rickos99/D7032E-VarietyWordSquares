using Game.Core.Communication;
using Game.Core.IO;
using Game.Core.Network;
using System.Net;

namespace Game.Core
{
    class GameClient
    {
        private readonly IInputOutput _inputOutput;
        private readonly Client _client;

        /*
        * FLOW:
        * 1. Connect to game
        * 2. Wait for incoming messages and act if the message is an question
        * 
        */
        public GameClient(IInputOutput inputOutput, IPEndPoint endPoint)
        {
            _inputOutput = inputOutput;
            _client = new Client(endPoint);
        }

        public void Start()
        {
            _client.OpenConnection();
            while (true)
            {
                var message = _client.ReadMessage();
                if (message is IQuestion question)
                {
                    var answer = _inputOutput.AskQuestion(question);
                    _client.SendMessage(new InformationMessage(answer));
                }
                else
                {
                    _inputOutput.DisplayMessage(message);
                }
                // TODO Make it possible to change exit loop, maybe with a GameHasEndedMessage?
            }
        }
    }
}
