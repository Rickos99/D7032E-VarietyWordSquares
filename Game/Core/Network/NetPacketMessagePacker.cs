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
            Type type = Type.GetType(packet.PayloadType);

            if (type == typeof(ClosedQuestion))
            {
                return DeserializePayload<ClosedQuestion>(packet.Payload);
            }
            else if (type == typeof(OpenQuestion))
            {
                return DeserializePayload<OpenQuestion>(packet.Payload);
            }
            else if(type == typeof(TimedOpenQuestion))
            {
                return DeserializePayload<TimedOpenQuestion>(packet.Payload);
            }
            else if (type == typeof(InformationMessage))
            {
                return DeserializePayload<InformationMessage>(packet.Payload);
            }

            throw new UnknownMessageException();
        }

        private static T DeserializePayload<T>(string payload)
        {
            return JsonSerializer.Deserialize<T>(payload);
        }
    }
}
