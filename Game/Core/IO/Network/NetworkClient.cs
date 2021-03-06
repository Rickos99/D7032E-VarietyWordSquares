using Game.Core.IO.Messages;
using Game.Core.Resources;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Game.Core.IO.Network
{
    public class NetworkClient
    {
        public int Port { get; private set; }
        public IPAddress IpAddress { get; private set; }

        private readonly TcpClient tcpClient = null;
        private readonly int _sizeOfNetworkBuffer = Settings.SizeOfNetworkBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class with 
        /// IP-address 127.0.0.1 and the specified port.
        /// </summary>
        /// <remarks>
        /// After initialization, plases call <see cref="OpenConnection"/> 
        /// to actuall open a connection to the specified destination
        /// </remarks>
        /// <param name="port">Port to connect to</param>
        public NetworkClient(int port) : this("127.0.0.1", port) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class and with a 
        /// specified IP-address and port.
        /// </summary>
        /// <remarks>
        /// After initialization, plases call <see cref="OpenConnection"/> 
        /// to actuall open a connection to the specified destination
        /// </remarks>
        /// <param name="ipAddress">IP-address to connect to</param>
        /// <param name="port">Port to connect to</param>
        public NetworkClient(string ipAddress, int port)
        {
            IpAddress = IPAddress.Parse(ipAddress);
            Port = port;
            tcpClient = new TcpClient();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class and with a 
        /// specified IP-address and port.
        /// </summary>
        /// <remarks>
        /// After initialization, plases call <see cref="OpenConnection"/> 
        /// to actuall open a connection to the specified destination
        /// </remarks>
        /// <param name="endPoint">Endpoint to connect to</param>
        public NetworkClient(IPEndPoint endPoint)
        {
            IpAddress = endPoint.Address;
            Port = endPoint.Port;
            tcpClient = new TcpClient();
        }

        /// <summary>
        /// Close connection to host.
        /// </summary>
        public void CloseConnection()
        {
            tcpClient.Close();
        }

        /// <summary>
        /// Open a connection to host. 
        /// <para>
        /// NOTE: If a connection already has been opened, no new connection will be 
        /// created and <see cref="OpenConnection"/> returns.
        /// </para>
        /// </summary>
        public void OpenConnection()
        {
            if (tcpClient.Connected) return;

            IPEndPoint endPoint = new IPEndPoint(IpAddress, Port);
            tcpClient.Connect(endPoint);
        }

        /// <summary>
        /// Read message from host
        /// </summary>
        /// <returns>The message that was read</returns>
        public IMessage ReadMessage()
        {
            var dataBuffer = new byte[_sizeOfNetworkBuffer];
            var bytes = NetStreamHelper.ReadOneJsonSequence(dataBuffer, tcpClient.GetStream());
            var packetAsJson = Encoding.UTF8.GetString(dataBuffer, 0, bytes);
            var packet = JsonSerializer.Deserialize<NetPacket<IMessage>>(packetAsJson);
            var message = NetPacketMessagePacker.Unpack(packet);

            return message;
        }

        /// <summary>
        /// Send message to host
        /// </summary>
        public void SendMessage(IMessage message)
        {
            var packet = new NetPacket<IMessage>(message);
            var packetAsJson = JsonSerializer.Serialize(packet);
            var packetBytes = Encoding.UTF8.GetBytes(packetAsJson);

            tcpClient.GetStream().Write(packetBytes);
        }
    }
}
