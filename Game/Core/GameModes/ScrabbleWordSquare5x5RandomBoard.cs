using Game.Core.Board;
using Game.Core.Board.Builders;
using Game.Core.GameModes.Rules;
using Game.Core.IO;
using Game.Core.Language;
using Game.Core.Network;
using System;

namespace Game.Core.GameModes
{
    sealed class ScrabbleWordSquare5x5RandomBoard : AbstractStandardWordSquare
    {
        private readonly SquareType[] _squareTypeToPlace = new SquareType[]
        {
            SquareType.DoubleLetter,
            SquareType.DoubleLetter,
            SquareType.DoubleLetter,
            SquareType.DoubleLetter,

            SquareType.TripleLetter,
            SquareType.TripleLetter,

            SquareType.DoubleWord,
            SquareType.DoubleWord,
            SquareType.DoubleWord,
            SquareType.DoubleWord,

            SquareType.TrippleWord,
            SquareType.TrippleWord,
            SquareType.TrippleWord,
        };

        private readonly int _boardSeed;

        public ScrabbleWordSquare5x5RandomBoard(
            IInputOutput io,
            Dictionary dictionary,
            TileSchema tileSchema,
            NetworkHost networkHost,
            int bots,
            int players,
            int? boardSeed = null,
            int? randomizationSeed = null)
            : base(io, tileSchema, networkHost, new ScrabbleWordSquareRules(dictionary), bots, players, true, randomizationSeed)
        {
            _boardSeed = boardSeed is null ? new Random().Next() : (int)boardSeed;
        }

        protected override StandardBoard BuildBoard()
        {
            return BoardBuilder
                .CreateStandardBuilder(5, 5)
                .UseRandomizedLayout(_squareTypeToPlace, _boardSeed)
                .DisplayTilePoints()
                .Build();
        }
    }
}
