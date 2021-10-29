using Game.Core;
using Game.Core.IO;
using Game.Core.IO.Messages;
using Game.Core.Resources;
using Game.UI.Console.Menus;
using System.Collections.Generic;
using System.Net;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            StartNewConsoleGame();
        }

        private static void StartNewConsoleGame()
        {
            var console = new GameConsoleIO();
            var HostOrClientQuestion = new ClosedQuestion("Start as host or client? [default: host]", new List<Choice>()
            {
                new Choice("H", "Host a game"),
                new Choice("C", "Connect to a game")
            });
            var answer = console.AskQuestion(HostOrClientQuestion);

            if (answer.ToUpper() == "C")
            {
                StartAsConsoleClient(console);
            }
            else
            {
                StartAsConsoleHost();
            }
        }

        private static void StartAsConsoleClient(GameConsoleIO console)
        {
            var endPoint = AskForHostEndPoint(console);
            new GameClient(console, endPoint).Start();
        }

        private static void StartAsConsoleHost()
        {
            MainMenu.Create().Show();
        }

        private static IPEndPoint AskForHostEndPoint(GameConsoleIO console)
        {
            var defaultIp = IPAddress.Loopback;
            var defaultPort = Settings.DefaultNetworkGamePort;
            var addressQuestion = new OpenQuestion($"Enter host address to connect to, including port number. [Default: {defaultIp}:{defaultPort}]");

            IPEndPoint endPoint = null;
            console.SendMessage(addressQuestion);
            console.GetInput((answer) =>
                IPEndPoint.TryParse(answer, out endPoint) || string.IsNullOrEmpty(answer)
            );
            return endPoint ?? new IPEndPoint(defaultIp, defaultPort);
        }
    }
}