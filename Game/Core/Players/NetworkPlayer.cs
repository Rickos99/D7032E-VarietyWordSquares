using Game.Core.Communication;
using Game.Core.Network;
using System.Net.Sockets;

namespace Game.Core.Players
{
    class NetworkPlayer : PlayerBase
    {
        private TcpClient _client;

        /// <summary>
        /// Initialize a new instance of the <see cref="NetworkPlayer"/> class
        /// </summary>
        /// <param name="client">
        ///     TcpClient in which the communication with player will be performed
        /// </param>
        public NetworkPlayer(TcpClient client)
        {
            _client = client;
        }

        public override string AskQuestion(IQuestion question)
        {
            SendMessage(question);
            var response = Host.ReadMessageFromClient(_client);
            return response.Content;
        }

        public override void SendMessage(IMessage message)
        {
            Host.SendMessageToClient(message, _client);
        }
    }
}
