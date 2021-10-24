using Game.Core.Board;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game.Core.Language
{
    public class TileSchema
    {
        /// <summary>
        /// Get number of tiles in <see cref="TileSchema"/>
        /// </summary>
        public int NumberOfTiles { get => Tiles.Count; }

        /// <summary>
        /// Get a list of all tiles in <see cref="TileSchema"/>
        /// </summary>
        public List<Tile> Tiles { get; private set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="TileSchema"/> class with
        /// a specified list of tiles.
        /// </summary>
        /// <param name="tiles">tiles in which the schema will contain</param>
        public TileSchema(List<Tile> tiles)
        {
            Tiles = tiles;
        }

        /// <summary>
        /// Check whether a letter exist in tileschema
        /// </summary>
        /// <remarks>
        /// The check is case insensitive.
        /// </remarks>
        /// <param name="letter">Letter to check</param>
        /// <returns><c>true</c> if letter exist in tileschema; Otherwise <c>false</c></returns>
        public bool TileExist(char letter)
        {
            letter = char.ToLower(letter);
            return Tiles.Any((tile) => tile.Letter == letter);
        }

        /// <summary>
        /// Load dictionary from file and replace current dictionary
        /// </summary>
        /// <param name="filepath">Path to file</param>
        public static TileSchema LoadFromFile(string filepath)
        {
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
