using Game.Core.Resources;
using System;
using System.Configuration;
using System.IO;

namespace Game.Core.GameModes
{
    abstract class GameModeBase
    {
        public void LoadResources()
        {
            var pathToDictionary = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    ConfigurationManager.AppSettings["Resources.DictionaryFolder"],
                    ConfigurationManager.AppSettings["Resources.DictionaryFile"]
                );
            var pathToTileSchema = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    ConfigurationManager.AppSettings["Resources.TileSchemaFolder"],
                    ConfigurationManager.AppSettings["Resources.TileSchemaFile"]
                );

            //Dictionary.Instance.LoadFromFile(pathToDictionary);
            //TileSchema.Instance.LoadFromFile(pathToTileSchema);
        }

        /// <summary>
        /// Start gamemode
        /// </summary>
        public abstract void Start();
        //public void UseBoard(IBoard board);
    }
}
