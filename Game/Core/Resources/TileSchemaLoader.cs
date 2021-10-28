using Game.Core.Board;
using Game.Core.Language;
using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Core.Resources
{
    /// <summary>
    /// Load a tileschema from a specified file.
    /// </summary>
    public class TileSchemaLoader : IResourceLoader<TileSchema>
    {
        public TileSchema LoadFromFile(string filepath)
        {
            if (!Path.IsPathRooted(filepath))
            {
                filepath = Path.Combine(Settings.AssemblyDirectory, filepath);
            }
            IEnumerable<string> lines;
            List<Tile> tiles = new List<Tile>();
            try
            {
                lines = File.ReadLines(filepath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    tiles.Add(GetTileFromString(line));
                }
            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException ||
                   e is FormatException)
                {
                    throw new FileLoadException("Invalid value format in tileschema file.", filepath);
                }

                throw new FileLoadException("The specified tileschema could not be loaded.", filepath);
            }

            return new TileSchema(tiles);
        }

        private static Tile GetTileFromString(string str)
        {
            string[] stringParts = str.Split('=', 2);
            char letter = char.ToLower(stringParts[0][0]);
            int points = int.Parse(stringParts[1]);

            return new Tile(letter, points);
        }
    }
}
