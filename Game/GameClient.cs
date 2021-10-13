using Game.Core.Communication;
using Game.Core.IO;
using Game.Core.Network;
using Game.Core.Players;
using Game.UI.Console.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class GameClient
    {
        /*
        * FLOW:
        * 1. Ask for connection settings and connect to game
        * 2. Wait for incoming messages and act if the message is an question
        * 
        */
        public GameClient()
        {
            var endpoint = AskForHostAddress();
            var client = new Client(endpoint);
            StartClient(client);
        }

        private static void StartClient(Client client)
        {
            client.OpenConnection();
            while (true)
            {
                var message = client.ReadMessage();
                if (message is IQuestion question)
                {
                    var answer = GameConsole.AskQuestion(question);
                    client.SendMessage(new InformationMessage(answer));
                }
                else
                {
                    GameConsole.DisplayMessage(message);
                }
                // TODO Make it possible to change exit loop, maybe with a GameHasEndedMessage?
            }
        }

        private static IPEndPoint AskForHostAddress()
        {
            var AddressQuestion = new OpenQuestion("Enter host address to connect to, including port number. [Default: 127.0.0.1:5500]");
            string input;
            IPEndPoint endPoint;
            do
            {
                input = GameConsole.AskQuestion(AddressQuestion);
                if (string.IsNullOrEmpty(input))
                {
                    return new IPEndPoint(IPAddress.Loopback, 5500);
                }
            } while (!IPEndPoint.TryParse(input, out endPoint));
            return endPoint;
        }
    }
}
