using Game.Core.Board;
using Game.Core.GameModes;
using Game.Core.IO;
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
            System.Console.WriteLine("An instance of Standard Word Square is starting");
            var boardLayout = new Square[3, 3] {
                    { new(SquareType.Regular), new(SquareType.Regular), new(SquareType.Regular)},
                    { new(SquareType.Regular), new(SquareType.Regular), new(SquareType.Regular)},
                    { new(SquareType.Regular), new(SquareType.Regular), new(SquareType.Regular)},
                };

            var gameInstance = new StandardWordSquare(
                new GameConsole(),
                Dictionary.LoadFromFile(Path.Combine(Settings.DictionaryFolder, Settings.DictionaryFile)),
                TileSchema.LoadFromFile(Path.Combine(Settings.TileSchemaFolder, Settings.TileSchemaFile)),
                boardLayout,
                new Host(5500),
                (int)Settings.NumberOfBots,
                (int)Settings.NumberOfPlayers,
                null
            );
            gameInstance.Start();
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
    }
}
