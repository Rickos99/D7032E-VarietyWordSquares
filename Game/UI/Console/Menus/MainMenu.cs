using Game.Core.Board;
using Game.Core.GameModes;
using Game.Core.GameModes.Rules;
using Game.Core.IO;
using Game.Core.Language;
using Game.Core.Network;
using Game.Core.Resources;
using System;
using System.Collections.Generic;
using System.IO;

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
                new MenuChoice("2", "Play ScrabbleSquares", PlayScrabbleSquares),
                new MenuChoice("3", "Settings", ShowSettingsMenu),
                new MenuChoice("!", "Exit application", ExitApplication),
            };

            return new ConsoleMenu(header, menuChoices);
        }

        private static void PlayStandardWordSquares()
        {
            var gameConsole = new GameConsole();
            var dictionary = LoadDictionary();
            var tileSchema = LoadTileSchema();
            var networkHost = CreateNetworkHost();

            var gameInstance = new StandardWordSquare(
                gameConsole,
                dictionary,
                tileSchema,
                networkHost,
                (int)Settings.NumberOfBots,
                (int)Settings.NumberOfPlayers,
                (int)Settings.BoardRowSize,
                (int)Settings.BoardColumnSize
            );
            gameInstance.Run();
        }

        private static void PlayScrabbleSquares()
        {
            System.Console.WriteLine("Not supported yet");
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

        private static Host CreateNetworkHost()
        {
            return new Host(5500);
        }
    }
}
