using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game.Core.Network
{
    public class NetPacket<T>
    {
        public string PayloadType { get; private set; }
        public string Payload { get; private set; }

        [JsonConstructor]
        public NetPacket(string payloadType, string payload)
        {
            if (string.IsNullOrWhiteSpace(payloadType))
            {
                throw new ArgumentException($"'{nameof(payloadType)}' cannot be null or whitespace.", nameof(payloadType));
            }

            if (string.IsNullOrWhiteSpace(payload))
            {
                throw new ArgumentException($"'{nameof(payload)}' cannot be null or whitespace.", nameof(payload));
            }

            PayloadType = payloadType;
            Payload = payload;
        }

        public NetPacket(T payload)
        {
            PayloadType = payload.GetType().FullName;
            Payload = JsonSerializer.Serialize(payload, payload.GetType());
        }
    }
}