using Game.Core.IO.Messages;
using Game.Core.IO.Network;
using System;
using System.Net.Sockets;

namespace Game.Core.IO
{
    class GameNetworkIO : IMessageIO
    {
        private TcpClient _tcpClient;

        public GameNetworkIO(TcpClient tcpClient)
        {
            _tcpClient = tcpClient ?? throw new ArgumentNullException(nameof(tcpClient));
        }

        public string AskQuestion(IQuestion question)
        {
            SendMessage(question);
            var response = NetworkHost.ReadMessageFromClient(_tcpClient);
            return response.Content;
        }

        public void SendMessage(IMessage message)
        {
            NetworkHost.SendMessageToClient(message, _tcpClient);
        }
    }
}
