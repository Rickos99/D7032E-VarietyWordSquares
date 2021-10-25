using Game.Core.Communication;
using Game.Core.Exceptions;
using System;
using System.Text.Json;

namespace Game.Core.Network
{
    // TODO add documentation
    public static class NetPacketMessagePacker
    {
        public static NetPacket<IMessage> Pack(IMessage message)
        {
            return new NetPacket<IMessage>(message);
        }

        public static IMessage Unpack(NetPacket<IMessage> packet)
        {
            Type type = Type.GetType(packet.PayloadType) ?? throw new UnknownMessageException();

            var msg = (IMessage)JsonSerializer.Deserialize(packet.Payload, type);
            return msg;
        }
    }
}
