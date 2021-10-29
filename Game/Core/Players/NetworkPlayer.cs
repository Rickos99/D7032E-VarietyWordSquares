﻿using Game.Core.IO.Messages;
using Game.Core.IO.Network;
using System.Net.Sockets;

namespace Game.Core.Players
{
    /// <summary>
    /// A human player located in a network.
    /// </summary>
    class NetworkPlayer : PlayerBase
    {
        private TcpClient _client;

        /// <summary>
        /// Initialize a new instance of the <see cref="NetworkPlayer"/> class
        /// </summary>
        /// <param name="client">
        /// TcpClient in which the communication with player will be performed
        /// </param>
        public NetworkPlayer(TcpClient client)
        {
            _client = client;
        }

        public override string AskQuestion(IQuestion question)
        {
            SendMessage(question);
            var response = NetworkHost.ReadMessageFromClient(_client);
            return response.Content;
        }

        public override void SendMessage(IMessage message)
        {
            NetworkHost.SendMessageToClient(message, _client);
        }
    }
}
