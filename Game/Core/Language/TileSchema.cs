using Game.Core.Board;
using System.Collections.Generic;
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
    }
}
