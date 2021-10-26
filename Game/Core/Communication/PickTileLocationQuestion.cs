using Game.Core.Board;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game.Core.Communication
{
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

        public PickTileLocationQuestion(Tile tile)
        {
            Choices = null;
            HasChoices = false;
            Content = $"Place {char.ToUpper(tile.Letter)} (syntax: row column. Example \"A0\")";
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
