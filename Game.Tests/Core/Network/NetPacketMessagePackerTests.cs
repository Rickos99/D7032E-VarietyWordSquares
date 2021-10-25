using FluentAssertions;
using Game.Core.Board;
using Game.Core.Communication;
using Game.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Game.Core.Network.Tests
{
    [TestClass]
    public class NetPacketMessagePackerTests
    {
        static object[] Messages
        {
            get
            {
                return new[]
                {
                    new object[]{new InformationMessage("The quick brown fox jumps over the lazy dog") },
                    new object[]{new OpenQuestion("Did the quick brown fox jump over the lazy dog?") },
                    new object[]{new TimedOpenQuestion("Did the quick brown fox jump over the lazy dog?", 5) },
                    new object[]{new ClosedQuestion("Is the fox brown?", new List<Choice>() {
                            new Choice("y", "Yes"),
                            new Choice("n", "No"),
                        })},
                    new object[]{new GameHasEndedMessage()},
                    new object[]{new PickATileQuestion()},
                    new object[]{new PickTileLocationQuestion(new Tile('A', 1))}
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Messages))]
        public void UnpackTest(IMessage message)
        {
            var netPacket = new NetPacket<IMessage>(message);

            var actual = NetPacketMessagePacker.Unpack(netPacket);
            actual.Should().BeEquivalentTo(message);
        }

        [DataTestMethod]
        [DynamicData(nameof(Messages))]
        public void PackTest(IMessage message)
        {
            var netPacket = new NetPacket<IMessage>(message);

            var actual = NetPacketMessagePacker.Pack(message);
            actual.Should().BeEquivalentTo(netPacket);
        }

        [TestMethod]
        public void UnpackTest_UnsupportedMessage()
        {
            var message = new UnSupportedMessage("The fox is brown");
            var netpacket = NetPacketMessagePacker.Pack(message);

            Assert.ThrowsException<UnknownMessageException>(() => NetPacketMessagePacker.Unpack(netpacket));
        }
    }


    class UnSupportedMessage : IMessage
    {
        public string Content { get; set; }

        public UnSupportedMessage(string message)
        {
            Content = message;
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}