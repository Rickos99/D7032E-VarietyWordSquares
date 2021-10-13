using Game.Core.Communication;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Game.Core.Network
{
    public class Host
    {
        public int Port { get; private set; }
        public IPAddress IpAddress { get; private set; }

        private readonly IList<TcpClient> clients = null;
        private readonly TcpListener tcpListener = null;

        /// <summary>
        ///  Initializes a new instance of the  <see cref="Host"/> class that listens for 
        ///  incoming connection attempts on IP-address 127.0.0.1 and the specified port 
        ///  number.
        /// </summary>
        /// <param name="port">Port of the host</param>
        public Host(int port) : this("127.0.0.1", port) { }

        /// <summary>
        ///  Initializes a new instance of the  <see cref="Host"/> class that listens for 
        ///  incoming connection attempts on the specified local IP-address and port number.
        /// </summary>
        /// <param name="ipAddress">Local IP-address to allow connections to</param>
        /// <param name="port">Port of the host</param>
        public Host(string ipAddress, int port)
        {
            IpAddress = IPAddress.Parse(ipAddress);
            Port = port;
            clients = new List<TcpClient>();

            var ipEndPoint = new IPEndPoint(IpAddress, Port);
            tcpListener = new TcpListener(ipEndPoint);
        }

        /// <summary>
        /// Disconnect all clients
        /// </summary>
        public void DisconnectAllClients()
        {
            foreach (var client in clients)
            {
                client.Close();
            }
        }

        /// <summary>
        /// Starts socket for listening of client requests
        /// </summary>
        /// <exception cref="SocketException"></exception>
        public void Start()
        {
            tcpListener.Start();
        }

        /// <summary>
        /// Stops socket for listening of client requests
        /// </summary>
        /// <exception cref="SocketException"></exception>
        public void Stop()
        {
            tcpListener.Stop();
        }

        /// <summary>
        /// Read an inoming message
        /// </summary>
        /// <returns>Recieved message</returns>
        public IMessage ReadMessage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read message from a specified client
        /// </summary>
        /// <returns>Recieved message</returns>
        public static IMessage ReadMessageFromClient(TcpClient client)
        {
            var dataBuffer = new byte[1024];
            var bytes = client.GetStream().Read(dataBuffer, 0, dataBuffer.Length);
            var packetAsJson = Encoding.UTF8.GetString(dataBuffer, 0, bytes);
            var packet = JsonSerializer.Deserialize<NetPacket<IMessage>>(packetAsJson);
            var message = NetPacketMessagePacker.Unpack(packet);

            return message;
        }

        /// <summary>
        /// Send a message to all clients
        /// </summary>
        /// <param name="message">The message to send</param>
        public void SendMessageToAllClients(IMessage message)
        {
            foreach (var client in clients)
            {
                SendMessageToClient(message, client);
            }
        }

        /// <summary>
        /// Send a message to a specific client
        /// </summary>
        /// <param name="message">The message to send</param>
        /// <param name="client">The client to send the message to</param>
        public static void SendMessageToClient(IMessage message, TcpClient client)
        {
            var packet = new NetPacket<IMessage>(message);
            var packetAsJson = JsonSerializer.Serialize(packet);
            var packetBytes = Encoding.UTF8.GetBytes(packetAsJson);
            client.GetStream().Write(packetBytes);
        }

        /// <summary>
        /// Wait for and accept any incoming connection
        /// </summary>
        public TcpClient WaitForIncomingConnection()
        {
            // TODO add to clients list
            var client = tcpListener.AcceptTcpClient();
            clients.Add(client);
            return client;
        }

        /// <summary>
        /// Wait for and accept a number of new incoming connections
        /// </summary>
        /// <param name="numberOfConnections">Number of connections to accept</param>
        public void WaitForIncomingConnections(int numberOfConnections)
        {
            throw new NotImplementedException();
        }
    }
}
