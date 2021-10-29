using Game.Core.GameModes;
using Game.Core.IO;
using Game.Core.IO.Network;
using Game.Core.Language;
using Game.Core.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Game.UI.Console.Menus
{
    class MainMenu
    {
        public static ConsoleMenu Create()
        {
            var header = "Menu:";
            var menuChoices = new List<MenuChoice>()
            {
                new MenuChoice("1", "Play standard Wordsquares", PlayStandardWordSquares),
                new MenuChoice("2", "Play ScrabbleSquares on standard board", PlayScrabbleSquares),
                new MenuChoice("3", "Play scrabbleSquares on 5x5 predefined ScrabbleBoard", PlayScrabbleSquares_5x5_PredefinedBoard),
                new MenuChoice("4", "Play scrabbleSuqares on 5x5 randomised ScrabbleBoard", PlayScrabbleSquares_5x5_RandomBoard),
                new MenuChoice("s", "Settings", ShowSettingsMenu),
                new MenuChoice("!", "Exit application", ExitApplication),
            };

            return new ConsoleMenu(header, menuChoices);
        }

        private static void PlayStandardWordSquares()
        {
            var gameConsole = new GameConsoleIO();
            var dictionary = LoadDictionary();
            var tileSchema = LoadTileSchema();
            var networkHost = CreateNetworkHost();

            var gameInstance = new StandardWordSquare(
                gameConsole,
                dictionary,
                tileSchema,
                networkHost,
                (int)Settings.NumberOfBots,
                (int)Settings.NumberOfRemotePlayers,
                (int)Settings.BoardRowSize,
                (int)Settings.BoardColumnSize
            );
            gameInstance.Run();
        }

        private static void PlayScrabbleSquares()
        {
            var gameConsole = new GameConsoleIO();
            var dictionary = LoadDictionary();
            var tileSchema = LoadTileSchema();
            var networkHost = CreateNetworkHost();

            var gameInstance = new ScrabbleWordSquare(
                gameConsole,
                dictionary,
                tileSchema,
                networkHost,
                (int)Settings.NumberOfBots,
                (int)Settings.NumberOfRemotePlayers,
                (int)Settings.BoardRowSize,
                (int)Settings.BoardColumnSize
            );
            gameInstance.Run();
        }

        private static void PlayScrabbleSquares_5x5_PredefinedBoard()
        {
            var gameConsole = new GameConsoleIO();
            var dictionary = LoadDictionary();
            var tileSchema = LoadTileSchema();
            var networkHost = CreateNetworkHost();

            var gameInstance = new ScrabbleWordSquare5x5PredefinedBoard(
                gameConsole,
                dictionary,
                tileSchema,
                networkHost,
                (int)Settings.NumberOfBots,
                (int)Settings.NumberOfRemotePlayers
            );
            gameInstance.Run();
        }

        private static void PlayScrabbleSquares_5x5_RandomBoard()
        {
            var gameConsole = new GameConsoleIO();
            var dictionary = LoadDictionary();
            var tileSchema = LoadTileSchema();
            var networkHost = CreateNetworkHost();

            var gameInstance = new ScrabbleWordSquare5x5RandomBoard(
                gameConsole,
                dictionary,
                tileSchema,
                networkHost,
                (int)Settings.NumberOfBots,
                (int)Settings.NumberOfRemotePlayers
            );
            gameInstance.Run();
        }

        private static void ShowSettingsMenu()
        {
            SettingsMenu.Create().Show();
        }

        private static void ExitApplication()
        {
            Environment.Exit(0);
        }

        private static Dictionary LoadDictionary()
        {
            return new DictionaryLoader().LoadFromFile(Path.Combine(Settings.DictionaryFolder, Settings.DictionaryFile));
        }

        private static TileSchema LoadTileSchema()
        {
            return new TileSchemaLoader().LoadFromFile(Path.Combine(Settings.TileSchemaFolder, Settings.TileSchemaFile));
        }

        private static NetworkHost CreateNetworkHost()
        {
            var port = Settings.DefaultNetworkGamePort;
            if (!Settings.AllowRemoteConnections)
            {
                return new NetworkHost(port);
            }

            var ipAddress = GetLocalIPAddress();
            return new NetworkHost(ipAddress, port);
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
