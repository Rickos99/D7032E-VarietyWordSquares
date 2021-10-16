using Game.Core.Board;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game.Core.Resources
{
    public class TileSchema
    {
        public int NumberOfTiles { get => _tiles.Count; }

        private List<Tile> _tiles = null;

        private static TileSchema instance = null;
        private static readonly object padlock = new object();

        private TileSchema()
        {
        }

        /// <summary>
        /// Get instance of <see cref="Dictionary"/> class
        /// </summary>
        public static TileSchema Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TileSchema();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Check whether a letter exist in tileschema
        /// </summary>
        /// <param name="letter">Letter to check</param>
        /// <returns><c>true</c> if letter exist in tileschema; Otherwise <c>false</c></returns>
        public bool TileExist(char letter)
        {
            return _tiles.Any((tile) => tile.Letter == char.ToLower(letter));
        }

        /// <summary>
        /// Get a list of all tiles in tileschema
        /// </summary>
        /// <returns>A list of all tiles in tileschema</returns>
        public List<Tile> GetAllTiles()
        {
            return _tiles;
        }

        /// <summary>
        /// Load dictionary from file and replace current dictionary
        /// </summary>
        /// <param name="filepath">Path to file</param>
        public void LoadFromFile(string filepath)
        {
            IEnumerable<string> lines;
            try
            {
                _tiles = new List<Tile>();
                lines = File.ReadLines(filepath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    _tiles.Add(GetTileFromString(line));
                }
            }
            catch(Exception e)
            {
                if(e is IndexOutOfRangeException || 
                   e is FormatException)
                {
                    throw new FileLoadException("Invalid value format in tileschema file.", filepath);
                }

                throw new FileLoadException("The specified tileschema could not be loaded.", filepath);
            }
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
