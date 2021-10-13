using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.Communication;
using FluentAssertions;

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
                    new object[]{new ClosedQuestion("Is the fox brown?", new List<Choice>() {
                            new Choice("y", "Yes"),
                            new Choice("n", "No"),
                        })}
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
    }
    
    // TODO Finish test class
    class UnSupportedMessage : IMessage
    {
        public string Content => throw new NotImplementedException();

        public UnSupportedMessage()
        {

        }

        public string GetMessageString()
        {
            throw new NotImplementedException();
        }
    }
}