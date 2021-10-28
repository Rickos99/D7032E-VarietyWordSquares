using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Game.Core.Resources
{
    static class Settings
    {
        public static uint BoardRowSize
        {
            get => uint.Parse(GetSetting("Board.Size.Rows"));
            set => SetSetting("Board.Size.Rows", value.ToString());
        }

        public static uint BoardColumnSize
        {
            get => uint.Parse(GetSetting("Board.Size.Columns"));
            set => SetSetting("Board.Size.Columns", value.ToString());
        }

        public static uint NumberOfPlayers
        {
            get => uint.Parse(GetSetting("Player.Humans"));
            set => SetSetting("Player.Humans", value.ToString());
        }

        public static uint NumberOfBots
        {
            get => uint.Parse(GetSetting("Player.Bots"));
            set => SetSetting("Player.Bots", value.ToString());
        }

        public static string DictionaryFolder
        {
            get => GetSetting("Resources.DictionaryFolder");
        }

        public static string DictionaryFile
        {
            get => GetSetting("Resources.DictionaryFile");
            set => SetSetting("Resources.DictionaryFile", value);
        }

        public static string BoardFolder
        {
            get => GetSetting("Resources.BoardFolder");
        }

        public static string BoardFile
        {
            get => GetSetting("Resources.BoardFile");
            set => SetSetting("Resources.BoardFile", value);
        }

        public static string TileSchemaFolder
        {
            get => GetSetting("Resources.TileSchemaFolder");
        }

        public static string TileSchemaFile
        {
            get => GetSetting("Resources.TileSchemaFile");
            set => SetSetting("Resources.TileSchemaFile", value);
        }

        public static int SizeOfNetworkBuffer
        {
            get => 4096;
        }

        public static string[] GetAvaliableDictionaryFiles()
        {
            return GetFileNamesInFolder(DictionaryFolder);
        }

        public static string[] GetAvaliableBoardFiles()
        {
            return GetFileNamesInFolder(BoardFolder);
        }

        public static string[] GetAvaliableTileSchemaFiles()
        {
            return GetFileNamesInFolder(TileSchemaFolder);
        }

        public static string AssemblyDirectory
        {
            get => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static string[] GetFileNamesInFolder(string directory)
        {
            if (!Path.IsPathRooted(directory))
            {
                directory = Path.Combine(AssemblyDirectory, directory);
            }
            return Directory.GetFiles(directory)
                .Select((f) => Path.GetFileName(f))
                .ToArray();
        }

        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static void SetSetting(string key, string value)
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
    }
}
