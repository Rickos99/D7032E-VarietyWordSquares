using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Core.Network;
using Game.Core.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using FluentAssertions;

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

        [DataTestMethod]
        [DynamicData("Messages")]
        [DoNotParallelize]
        public void ClientHost_test(IMessage message)
        {
            int port = 5500;
            Host host = new Host(port);
            Client client = new Client(port);
            IMessage recievedMessage = null;

            host.Start();
            var connectionTask = Task.Run(() => host.WaitForIncomingConnection());
            client.OpenConnection();
            var hostClient = connectionTask.Result;

            // Message: Host -> Client
            Host.SendMessageToClient(message, hostClient);
            recievedMessage = client.ReadMessage();
            recievedMessage.Should().BeEquivalentTo(message);

            // Message: Client -> Host
            client.SendMessage(message);
            recievedMessage = Host.ReadMessageFromClient(hostClient);
            recievedMessage.Should().BeEquivalentTo(message);

            host.DisconnectAllClients();
            client.CloseConnection();
            host.Stop();
        }
    }
}