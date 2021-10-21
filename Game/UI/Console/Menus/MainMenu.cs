using System;
using System.Collections.Generic;

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
            System.Console.WriteLine("Not supported yet");
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
