using FluentAssertions;
using Game.Core.Communication;
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

        private Host host;
        private Client client;
        private TcpClient hostClient;

        [TestInitialize()]
        public void Startup()
        {
            int port = 5500;
            host = new Host(port);
            client = new Client(port);

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

            // Message: Host -> Client
            Host.SendMessageToClient(message, hostClient);
            recievedMessage = client.ReadMessage();
            recievedMessage.Should().BeEquivalentTo(message);

            // Message: Client -> Host
            client.SendMessage(message);
            recievedMessage = Host.ReadMessageFromClient(hostClient);
            recievedMessage.Should().BeEquivalentTo(message);
        }

        [DataTestMethod]
        [DynamicData(nameof(Messages))]
        [DoNotParallelize]
        public void ClientHostTest_TestMessageQueue(IMessage message)
        {
            IMessage recievedMessage;
            int messagesToSend = 3;

            // Send messages: Host -> Client
            for (int i = 0; i < messagesToSend; i++)
            {
                Host.SendMessageToClient(message, hostClient);
            }

            // Recieve messages: Client
            for (int i = 0; i < messagesToSend; i++)
            {
                recievedMessage = client.ReadMessage();
                recievedMessage.Should().BeEquivalentTo(message);
            }

            // Send messages: Client -> Host
            for (int i = 0; i < messagesToSend; i++)
            {
                client.SendMessage(message);
            }

            // Recieve messages: Host
            for (int i = 0; i < messagesToSend; i++)
            {
                recievedMessage = Host.ReadMessageFromClient(hostClient);
                recievedMessage.Should().BeEquivalentTo(message);
            }
        }
    }
}