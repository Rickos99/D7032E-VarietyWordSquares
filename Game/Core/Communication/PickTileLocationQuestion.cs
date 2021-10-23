using Game.Core.Board;
using System.Collections.Generic;

namespace Game.Core.Communication
{
    class PickTileLocationQuestion : IQuestion
    {
        public IList<Choice> Choices { get; private set; }

        public bool HasChoices { get; private set; }

        public string Content { get; private set; }

        public PickTileLocationQuestion(Tile tile)
        {
            Choices = null;
            HasChoices = false;
            Content = $"Place {tile.Letter} (syntax: row column. Example \"A0\")";
        }

        public string GetMessageString()
        {
            return Content;
        }
    }
}
