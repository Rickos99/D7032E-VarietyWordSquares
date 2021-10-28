using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game.Core.Network
{
    /// <summary>
    /// Package an item to be send over a network connection while still preserving an objects data and type. Does support inheritence.
    /// </summary>
    /// <typeparam name="T">Type of object to package.</typeparam>
    public class NetPacket<T>
    {
        /// <summary>
        /// Gets the fully qualified name of the type <see cref="T"/>, including its namespace but not its assembly.
        /// </summary>
        public string PayloadType { get; private set; }

        /// <summary>
        /// JSON serialized value of <see cref="T"/>.
        /// </summary>
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

        /// <summary>
        /// Initialize a new instance of the <see cref="NetPacket{T}"/> class with a specified payload.
        /// </summary>
        /// <param name="payload">Object to package.</param>
        public NetPacket(T payload)
        {
            PayloadType = payload.GetType().FullName;
            Payload = JsonSerializer.Serialize(payload, payload.GetType());
        }
    }
}