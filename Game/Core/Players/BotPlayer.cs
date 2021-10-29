using Game.Core.Board;
using Game.Core.IO.Messages;
using Game.Core.Language;
using System;
using System.Linq;

namespace Game.Core.Players
{
    /// <summary>
    /// A non-human player. It makes decisions based on a seed and the data
    /// it has been provided with.
    /// </summary>
    class BotPlayer : PlayerBase
    {
        private readonly Random _rng;
        private readonly StandardBoard _board;

        /// <summary>
        /// Initialize a new instance of the <see cref="BotPlayer"/> class with a 
        /// board, tileSchema, and seed used for randomization of bot actions.
        /// </summary>
        /// <remarks>
        /// Behavior of the bot is only determanistic if the method paramaters
        /// are kept constant.
        /// </remarks>
        /// <param name="board">Board in which the bot will have as a decision base </param>
        /// <param name="rngSeed">Seed to use in decisions</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BotPlayer(StandardBoard board, int rngSeed)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            _rng = new Random(rngSeed);
            _board = board;
        }

        public override string AskQuestion(IQuestion question)
        {
            throw new NotSupportedException();
        }

        public override void SendMessage(IMessage message) { }

        public override BoardLocation PickTileLocation(Tile tile, bool showTilePoints)
        {
            var emptyLocations = _board.GetAllEmptyLocations().ToList();
            var locationIndex = _rng.Next(emptyLocations.Count - 1);
            return emptyLocations[locationIndex];
        }

        public override Tile PickTile(TileSchema tileSchema)
        {
            var letterIndex = _rng.Next(tileSchema.NumberOfTiles - 1);
            return tileSchema.Tiles[letterIndex];
        }
    }
}
