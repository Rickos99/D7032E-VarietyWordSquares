using Game.Core.Communication;
using Game.Core.Exceptions;
using Game.Core.IO;
using Game.Core.Network;
using System;
using System.Net;

namespace Game.Core
{
    class GameClient
    {
        private readonly IInputOutput _inputOutput;
        private readonly NetworkClient _client;

        public GameClient(IInputOutput inputOutput, IPEndPoint endPoint)
        {
            _inputOutput = inputOutput;
            _client = new NetworkClient(endPoint);
        }

        public void Start()
        {
            try
            {
                _client.OpenConnection();
            }
            catch
            {
                _inputOutput.DisplayMessage(new InformationMessage($"Unable to open a connection to {_client.IpAddress}:{_client.Port}"));
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
                    _inputOutput.DisplayMessage(new InformationMessage("Connection to host has been closed."));
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
                    _inputOutput.DisplayMessage(m);
                    break;
                }
                else
                {
                    _inputOutput.DisplayMessage(message);
                }
            }
        }

        private void DisplayErrorMessage(Exception e)
        {
            _inputOutput.DisplayMessage(new InformationMessage($"An unexpected error occured.\n\t- {e.Message}"));
        }
    }
}
