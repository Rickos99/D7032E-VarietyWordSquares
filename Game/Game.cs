using Game.Core.Communication;
using Game.UI.Console.Menus;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Game
    {
        public Game(string[] args)
        {

            GetMainMenu().Show();
        }

        private ConsoleMenu GetMainMenu()
        {
            var header = "Menu:";
            var menuChoices = new List<MenuChoice>()
            {
                new MenuChoice("1", "Gamemode 1", SimpleMenuAction),
                new MenuChoice("2", "Gamemode 2", SimpleMenuAction),
                new MenuChoice("3", "Gamemode 3", SimpleMenuAction),
                new MenuChoice("4", "Settings", () => GetSettingsMenu().Show()),
                new MenuChoice("!", "Exit application", () => Environment.Exit(0)),
            };
            return new ConsoleMenu(header, menuChoices);
        }

        private ConsoleMenu GetSettingsMenu()
        {
            var appSettings = ConfigurationManager.AppSettings;

            var header = "Settings";
            var menuChoices = new List<MenuChoice>()
            {
                new MenuChoice("1", $"Board size (rows/columns): {appSettings["Board.Size.Rows"]}/{appSettings["Board.Size.Columns"]}", SimpleMenuAction),
                new MenuChoice("2", $"Language: {appSettings["Language.Dictionary"]}", SimpleMenuAction),
                new MenuChoice(
                    "3",
                    () => $"Number of players: {ConfigurationManager.AppSettings["Player.Humans"]}",
                    () => {
                        Console.Write("Enter a new value for Player.Humans: ");
                        AddUpdateAppSettings("Player.Humans", Console.ReadLine());
                    }),
                new MenuChoice(
                    "4",
                    $"Number of bots: {appSettings["Player.Bots"]}",
                    () => {
                        Console.Write("Enter a new value for Player.Bots: ");
                        AddUpdateAppSettings("Player.Bots", Console.ReadLine());
                    }),
                new MenuChoice("5", $"Testing (remove for final release): false", SimpleMenuAction),
                new ExitMenuChoice("Go back")
            };
            return new ConsoleMenu(header, menuChoices);
        }

        private void SimpleMenuAction()
        {

        }

        private void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

    }
}
