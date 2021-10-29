using Game.Core.Board;
using Game.Core.Board.Builders;
using Game.Core.GameModes.Rules;
using Game.Core.IO;
using Game.Core.IO.Network;
using Game.Core.Language;

namespace Game.Core.GameModes
{
    sealed class ScrabbleWordSquare : AbstractStandardWordSquare
    {
        private readonly int _numberOfBoardColumns;
        private readonly int _numberOfBoardRows;

        public ScrabbleWordSquare(
            IMessageIO io,
            Dictionary dictionary,
            TileSchema tileSchema,
            NetworkHost networkHost,
            int bots,
            int players,
            int rows,
            int columns,
            int? randomizationSeed = null)
            : base(io, tileSchema, networkHost, new ScrabbleWordSquareRules(dictionary), bots, players, true, randomizationSeed)
        {
            _numberOfBoardColumns = rows;
            _numberOfBoardRows = columns;
        }

        protected override StandardBoard BuildBoard()
        {
            return BoardBuilder
                .CreateStandardBuilder(_numberOfBoardRows, _numberOfBoardColumns)
                .UseUniformLayout(SquareType.Regular)
                .DisplayTilePoints()
                .Build();
        }
    }
}
