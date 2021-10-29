using Game.Core.Board;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game.Core.IO.Messages
{
    /// <summary>
    /// A question which prompts a player to pick the location for a tile.
    /// </summary>
    public class PickTileLocationQuestion : IQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public string Content { get; private set; }

        [JsonConstructor]
        public PickTileLocationQuestion(string content)
        {
            Choices = null;
            HasChoices = false;
            Content = content;
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="PickTileLocationQuestion"/> class
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="showTilePoints">Indicates whether to print point value of tile. Set to <c>true</c> if points should be shown, otherwise false.</param>
        public PickTileLocationQuestion(Tile tile, bool showTilePoints)
        {
            Choices = null;
            HasChoices = false;

            var tilePointsString = showTilePoints ? $" [{tile.Points}]" : "";
            var letter = char.ToUpper(tile.Letter);
            Content = $"Place {letter}{tilePointsString} (syntax: row column. Example \"A0\")";
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
