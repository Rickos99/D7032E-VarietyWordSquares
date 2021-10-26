using Game.Core.Board;
using Game.Core.Board.Builders;
using Game.Core.Communication;
using Game.Core.GameModes.Rules;
using Game.Core.IO;
using Game.Core.Language;
using Game.Core.Network;
using Game.Core.Players;

namespace Game.Core.GameModes
{
    sealed class StandardWordSquare : AbstractStandardWordSquare
    {
        private readonly int _numberOfBoardColumns;
        private readonly int _numberOfBoardRows;

        public StandardWordSquare(
            IInputOutput io,
            Dictionary dictionary,
            TileSchema tileSchema,
            NetworkHost networkHost,
            int bots,
            int players,
            int rows,
            int columns,
            int? randomizationSeed = null)
            : base(io, tileSchema, networkHost, new StandardWordSquareRules(dictionary), bots, players, randomizationSeed)
        {
            _numberOfBoardColumns = rows;
            _numberOfBoardRows = columns;
        }

        protected override StandardBoard BuildBoard()
        {
            return BoardBuilder
                .CreateStandardBuilder(_numberOfBoardRows, _numberOfBoardColumns)
                .UseUniformLayout(SquareType.Regular)
                .HideTilePoints()
                .Build();
        }

        protected override Tile LetPlayerPickTile(PlayerBase player)
        {
            player.SendMessage(new InformationMessage(_tileSchema.GetAsStringWithOnlyLetters()));
            return base.LetPlayerPickTile(player);
        }
    }
}
