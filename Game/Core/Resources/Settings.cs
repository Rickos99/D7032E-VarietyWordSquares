using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Game.Core.Resources
{
    /// <summary>
    /// Access application settings, both constants and settings stored in the app.config file.
    /// </summary>
    static class Settings
    {
        /// <summary>
        /// Number of rows there should be on boards when they are defined by the user.
        /// </summary>
        public static uint BoardRowSize
        {
            get => uint.Parse(GetSetting("Board.Size.Rows"));
            set => SetSetting("Board.Size.Rows", value.ToString());
        }

        /// <summary>
        /// Number of columns there should be on boards when they are defined by the user.
        /// </summary>
        public static uint BoardColumnSize
        {
            get => uint.Parse(GetSetting("Board.Size.Columns"));
            set => SetSetting("Board.Size.Columns", value.ToString());
        }

        /// <summary>
        /// The number of remote players to allow in a game.
        /// </summary>
        public static uint NumberOfRemotePlayers
        {
            get => uint.Parse(GetSetting("Player.Remote"));
            set => SetSetting("Player.Remote", value.ToString());
        }

        /// <summary>
        /// Number of bots to allow in a game.
        /// </summary>
        public static uint NumberOfBots
        {
            get => uint.Parse(GetSetting("Player.Bots"));
            set => SetSetting("Player.Bots", value.ToString());
        }

        /// <summary>
        /// Indicates whether to allow connections from clients from IP-adresses other than localhost.
        /// </summary>
        public static bool AllowRemoteConnections
        {
            get => bool.Parse(GetSetting("Network.AllowRemoteConnection"));
            set => SetSetting("Network.AllowRemoteConnection", value.ToString());
        }

        /// <summary>
        /// Folder in which all dictionaries are stores. It is relative to the assembly location.
        /// </summary>
        public static string DictionaryFolder
        {
            get => GetSetting("Resources.DictionaryFolder");
        }

        /// <summary>
        /// Name of current dictionaryfile in the <see cref="DictionaryFolder"/>.
        /// </summary>
        public static string DictionaryFile
        {
            get => GetSetting("Resources.DictionaryFile");
            set => SetSetting("Resources.DictionaryFile", value);
        }

        /// <summary>
        /// Folder in which all userdefined boards are stored. It is relative to the assembly location.
        /// </summary>
        public static string BoardFolder
        {
            get => GetSetting("Resources.BoardFolder");
        }

        /// <summary>
        /// Name of current boardfile in the <see cref="BoardFolder"/>.
        /// </summary>
        public static string BoardFile
        {
            get => GetSetting("Resources.BoardFile");
            set => SetSetting("Resources.BoardFile", value);
        }

        /// <summary>
        /// Folder in which all tileschemas are stored. It is relative to the assembly location.
        /// </summary>
        public static string TileSchemaFolder
        {
            get => GetSetting("Resources.TileSchemaFolder");
        }

        /// <summary>
        /// Name of current tileschema file in the <see cref="TileSchemaFolder"/>.
        /// </summary>
        public static string TileSchemaFile
        {
            get => GetSetting("Resources.TileSchemaFile");
            set => SetSetting("Resources.TileSchemaFile", value);
        }

        /// <summary>
        /// Size of network buffers when sending and reciveing messages.
        /// </summary>
        public static int SizeOfNetworkBuffer
        {
            get => 4096;
        }

        /// <summary>
        /// Default port number when opening a network connection.
        /// </summary>
        public static int DefaultNetworkGamePort
        {
            get => 5500;
        }

        /// <summary>
        /// Get all avaliable files in the <see cref="DictionaryFolder"/>.
        /// </summary>
        /// <returns>All avaliable files in the <see cref="DictionaryFolder"/>.</returns>
        public static string[] GetAvaliableDictionaryFiles()
        {
            return GetFileNamesInFolder(DictionaryFolder);
        }

        /// <summary>
        /// Get all avaliable files in the <see cref="BoardFolder"/>.
        /// </summary>
        /// <returns>All avaliable files in the <see cref="BoardFolder"/>.</returns>
        public static string[] GetAvaliableBoardFiles()
        {
            return GetFileNamesInFolder(BoardFolder);
        }

        /// <summary>
        /// Get all avaliable files in the <see cref="TileSchemaFolder"/>.
        /// </summary>
        /// <returns>All avaliable files in the <see cref="TileSchemaFolder"/>.</returns>
        public static string[] GetAvaliableTileSchemaFiles()
        {
            return GetFileNamesInFolder(TileSchemaFolder);
        }

        /// <summary>
        /// Get the absolute path to current assembly.
        /// </summary>
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
