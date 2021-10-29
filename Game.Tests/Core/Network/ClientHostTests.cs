using FluentAssertions;
using Game.Core.IO.Messages;
using Game.Core.IO.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Game.Core.Network.Tests
{
    [TestClass]
    public class ClientHostTests
    {
        static object[] Messages
        {
            get
            {
                return new[]
                {
                    new object[]{new InformationMessage("The quick brown fox jumps over the lazy dog") },
                    new object[]{new OpenQuestion("Did the quick brown fox jump over the lazy dog?") },
                    new object[]{new ClosedQuestion("Is the fox brown?", new List<Choice>() {
                            new Choice("y", "Yes"),
                            new Choice("n", "No"),
                        })}
                };
            }
        }

        private NetworkHost host;
        private NetworkClient client;
        private TcpClient hostClient;

        [TestInitialize()]
        public void Startup()
        {
            int port = 5500;
            host = new NetworkHost(port);
            client = new NetworkClient(port);

            host.Start();
            var connectionTask = Task.Run(() => host.WaitForIncomingConnection());
            client.OpenConnection();
            hostClient = connectionTask.Result;
        }

        [TestCleanup()]
        public void Cleanup()
        {
            host.DisconnectAllClients();
            client.CloseConnection();
            host.Stop();

            host = null;
            client = null;
            hostClient = null;
        }

        [DataTestMethod]
        [DynamicData(nameof(Messages))]
        [DoNotParallelize]
        public void ClientHost_test(IMessage message)
        {
            IMessage recievedMessage;

            // Message: NetworkHost -> NetworkClient
            NetworkHost.SendMessageToClient(message, hostClient);
            recievedMessage = client.ReadMessage();
            recievedMessage.Should().BeEquivalentTo(message);

            // Message: NetworkClient -> NetworkHost
            client.SendMessage(message);
            recievedMessage = NetworkHost.ReadMessageFromClient(hostClient);
            recievedMessage.Should().BeEquivalentTo(message);
        }

        [DataTestMethod]
        [DynamicData(nameof(Messages))]
        [DoNotParallelize]
        public void ClientHostTest_TestMessageQueue(IMessage message)
        {
            IMessage recievedMessage;
            int messagesToSend = 3;

            // Send messages: NetworkHost -> NetworkClient
            for (int i = 0; i < messagesToSend; i++)
            {
                NetworkHost.SendMessageToClient(message, hostClient);
            }

            // Recieve messages: NetworkClient
            for (int i = 0; i < messagesToSend; i++)
            {
                recievedMessage = client.ReadMessage();
                recievedMessage.Should().BeEquivalentTo(message);
            }

            // Send messages: NetworkClient -> NetworkHost
            for (int i = 0; i < messagesToSend; i++)
            {
                client.SendMessage(message);
            }

            // Recieve messages: NetworkHost
            for (int i = 0; i < messagesToSend; i++)
            {
                recievedMessage = NetworkHost.ReadMessageFromClient(hostClient);
                recievedMessage.Should().BeEquivalentTo(message);
            }
        }
    }
}