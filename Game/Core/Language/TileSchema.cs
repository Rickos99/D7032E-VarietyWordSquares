using Game.Core.Board;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Language
{
    /// <summary>
    /// Used to map a set of charactes e.g., an alphabet to a point value.
    /// </summary>
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
        /// Get <see cref="TileSchema"/> as a string of tiles in alphabetical order, grouped by <see cref="Tile.Points"/>.
        /// </summary>
        /// <returns><see cref="TileSchema"/> as a string of tiles in alphabetical order, grouped by <see cref="Tile.Points"/></returns>
        public string GetAsStringGroupedByPoints()
        {
            var groups = Tiles.GroupBy(tile => tile.Points)
                              .OrderBy(group => group.Key);

            var sb = new StringBuilder();
            sb.AppendLine("Avaliable letters to choose from:");
            foreach (var group in groups)
            {
                var points = group.Key;
                var orderedGroup = group.OrderBy(tile => tile.Letter);

                sb.Append($"  {points} {(points < 2 ? "point" : "points")}:");
                foreach (var item in group)
                {
                    sb.Append($" {char.ToUpper(item.Letter)}");
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get <see cref="TileSchema"/> as a string of tiles in alphabetical order without the <see cref="Tile.Points"/>.
        /// </summary>
        /// <returns><see cref="TileSchema"/> as a string of tiles in alphabetical order without the <see cref="Tile.Points"/>.</returns>
        public string GetAsStringWithOnlyLetters()
        {
            var orderedTiles = Tiles.OrderBy(tile => tile.Letter);

            var sb = new StringBuilder();
            sb.AppendLine("Avaliable letters to choose from:");
            foreach (var tile in orderedTiles)
            {
                sb.Append($" {char.ToUpper(tile.Letter)}");
            }

            return sb.ToString();
        }
    }
}
