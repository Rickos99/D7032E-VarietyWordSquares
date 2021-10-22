﻿using Game.Core.Communication;
using Game.Core.IO;
using Game.Core.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Game.UI.Console.Menus
{
    class SettingsMenu
    {
        private readonly static GameConsole _console = new GameConsole();

        public static ConsoleMenu Create()
        {
            var appSettings = ConfigurationManager.AppSettings;

            var header = "Current settings";
            var menuChoices = new List<MenuChoice>()
            {
                new MenuChoice(
                    "1",
                    () => $"Board size (rows/columns): {Settings.BoardRowSize}/{Settings.BoardColumnSize}",
                    ChangeBoardSize),
                new MenuChoice(
                    "2",
                    () => $"Current dictionary: {Path.GetFileNameWithoutExtension(Settings.DictionaryFile)}",
                    ChangeDictionary
                    ),
                new MenuChoice(
                    "3",
                    () => $"Alphabet: {Path.GetFileNameWithoutExtension(Settings.TileSchemaFile)}",
                    ChangeTileSchema
                    ),
                new MenuChoice(
                    "4",
                    () => $"Number of players: {Settings.NumberOfPlayers}",
                    ChangeNumberOfPlayers
                    ),
                new MenuChoice(
                    "5",
                    () => $"Number of bots: {Settings.NumberOfBots}",
                    ChangeNumberOfBots
                    ),
                new ExitMenuChoice("Go back")
            };
            return new ConsoleMenu(header, menuChoices);
        }

        private static void ChangeBoardSize()
        {
            string input = _console.AskQuestion(new OpenQuestion("Enter new board size (rows/columns):"));
            uint row, col;
            try
            {
                var values = input.Split('/', 2);
                row = uint.Parse(values[0]);
                col = uint.Parse(values[1]);
            }
            catch (Exception)
            {
                TellUser_InvalidInput();
                return;
            }
            Settings.BoardRowSize = row;
            Settings.BoardColumnSize = col;
        }

        private static void ChangeDictionary()
        {
            var avaliableDictionaries = Settings.GetAvaliableDictionaryFiles();
            string input = _console.AskQuestion(new ClosedQuestion(
                    "Select a dictionary to use:",
                    CreateChoices(avaliableDictionaries)
                ));
            if (!uint.TryParse(input, out uint dictionaryIndex))
            {
                TellUser_InvalidInput();
                return;
            }
            Settings.DictionaryFile = avaliableDictionaries[dictionaryIndex];
        }

        private static void ChangeTileSchema()
        {
            var avaliableTileSchemas = Settings.GetAvaliableTileSchemaFiles();
            string input = _console.AskQuestion(new ClosedQuestion(
                    "Select an alphabet to use:",
                    CreateChoices(avaliableTileSchemas)
                ));
            if (!uint.TryParse(input, out uint tileSchemaIndex))
            {
                TellUser_InvalidInput();
                return;
            }
            Settings.TileSchemaFile = avaliableTileSchemas[tileSchemaIndex];
        }

        private static void ChangeNumberOfPlayers()
        {
            string input = _console.AskQuestion(new OpenQuestion("Enter the number of players:"));
            if (!uint.TryParse(input, out uint players))
            {
                TellUser_InvalidInput();
                return;
            }

            Settings.NumberOfPlayers = players;
        }

        private static void ChangeNumberOfBots()
        {
            string input = _console.AskQuestion(new OpenQuestion("Enter the number of bots:"));
            if (!uint.TryParse(input, out uint bots))
            {
                TellUser_InvalidInput();
                return;
            }

            Settings.NumberOfBots = bots;
        }

        private static void TellUser_InvalidInput()
        {
            _console.DisplayMessage(new InformationMessage("Invalid input"));
        }

        private static List<Choice> CreateChoices(string[] strs)
        {
            return strs.Select((s, i) => new Choice(i.ToString(), s)).ToList();
        }
    }
}