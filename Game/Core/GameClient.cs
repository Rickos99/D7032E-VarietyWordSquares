using Game.Core.Exceptions;
using Game.Core.IO;
using Game.Core.IO.Messages;
using Game.Core.IO.Network;
using System;
using System.Net;

namespace Game.Core
{
    /// <summary>
    /// Let a user establish a connection to an already running game.
    /// </summary>
    class GameClient
    {
        private readonly IMessageIO _inputOutput;
        private readonly NetworkClient _client;

        /// <summary>
        /// Initialize a new instance of the <see cref="GameClient"/> class.
        /// </summary>
        /// <param name="inputOutput">IO interface to use.</param>
        /// <param name="endPoint">Endpoint to connect to.</param>
        public GameClient(IMessageIO inputOutput, IPEndPoint endPoint)
        {
            _inputOutput = inputOutput;
            _client = new NetworkClient(endPoint);
        }

        /// <summary>
        /// Open a connection and start listening for incoming messages.
        /// </summary>
        public void Start()
        {
            try
            {
                _client.OpenConnection();
            }
            catch
            {
                _inputOutput.SendMessage(new InformationMessage($"Unable to open a connection to {_client.IpAddress}:{_client.Port}"));
                return;
            }
            while (true)
            {
                IMessage message;
                try
                {
                    message = _client.ReadMessage();
                }
                catch (ConnectionClosedException)
                {
                    _inputOutput.SendMessage(new InformationMessage("Connection to host has been closed."));
                    break;
                }
                catch (Exception e)
                {
                    DisplayErrorMessage(e);
                    break;
                }

                if (message is IQuestion question)
                {
                    var answer = _inputOutput.AskQuestion(question);
                    try
                    {
                        _client.SendMessage(new InformationMessage(answer));
                    }
                    catch (Exception e)
                    {
                        DisplayErrorMessage(e);
                        break;
                    }
                }
                else if (message is GameHasEndedMessage m)
                {
                    _inputOutput.SendMessage(m);
                    break;
                }
                else
                {
                    _inputOutput.SendMessage(message);
                }
            }
        }

        private void DisplayErrorMessage(Exception e)
        {
            _inputOutput.SendMessage(new InformationMessage($"An unexpected error occured.\n\t- {e.Message}"));
        }
    }
}
