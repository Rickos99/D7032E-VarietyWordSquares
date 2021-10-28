using Game.Core.Board;
using Game.Core.Board.Builders;
using Game.Core.GameModes.Rules;
using Game.Core.IO;
using Game.Core.Language;
using Game.Core.Network;

namespace Game.Core.GameModes
{
    sealed class ScrabbleWordSquare5x5PredefinedBoard : AbstractStandardWordSquare
    {
        private readonly SquareType[,] _predefinedLayout = new SquareType[5,5]
        {
            { SquareType.DoubleWord, SquareType.Regular, SquareType.TrippleWord, SquareType.Regular, SquareType.DoubleWord },
            { SquareType.Regular, SquareType.DoubleLetter, SquareType.Regular, SquareType.DoubleLetter, SquareType.Regular },
            { SquareType.TripleLetter, SquareType.Regular, SquareType.TrippleWord, SquareType.Regular, SquareType.TripleLetter },
            { SquareType.Regular, SquareType.DoubleLetter, SquareType.Regular, SquareType.DoubleLetter, SquareType.Regular },
            { SquareType.DoubleWord, SquareType.Regular, SquareType.TrippleWord, SquareType.Regular, SquareType.DoubleWord }
        };

        public ScrabbleWordSquare5x5PredefinedBoard(
            IInputOutput io,
            Dictionary dictionary,
            TileSchema tileSchema,
            NetworkHost networkHost,
            int bots,
            int players,
            int? randomizationSeed = null)
            : base(io, tileSchema, networkHost, new ScrabbleWordSquareRules(dictionary), bots, players, true, randomizationSeed)
        {
        }

        protected override StandardBoard BuildBoard()
        {
            return BoardBuilder
                .CreateStandardBuilder(5, 5)
                .UseLayout(_predefinedLayout)
                .DisplayTilePoints()
                .Build();
        }
    }
}
