using Game.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Game.Core.Board.Builders
{
    public class StandardBoardBuilder : IBoardBuilder
    {
        private int _boardRows;
        private int _boardColumns;
        private bool _boardBuilt;
        private bool _displayPointsOnBoard;
        private List<Action<Square[,]>> _buildActions = new();

        /// <summary>
        /// Initialize a new instance of the <see cref="StandardBoardBuilder"/> class
        /// with a predefined number of rows and columns.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public StandardBoardBuilder(int rows, int columns)
        {
            if (rows < 1) throw new ArgumentOutOfRangeException(nameof(rows), "Must be grater than 0");
            if (columns < 1) throw new ArgumentOutOfRangeException(nameof(columns), "Must be grater than 0");

            _boardRows = rows;
            _boardColumns = columns;
        }

        public StandardBoard Build()
        {
            if (_boardBuilt) throw new BoardAlreadyBuiltException();

            var board = new Square[_boardRows, _boardColumns];
            foreach (var action in _buildActions)
            {
                action.Invoke(board);
            }

            _boardBuilt = true;
            return new StandardBoard(board, _displayPointsOnBoard);
        }

        public IBoardBuilder FillWith(Tile[,] tiles)
        {
            if (!DimensionsMatchWithBoard(tiles)) throw new ArgumentOutOfRangeException(nameof(tiles));

            _buildActions.Add(board => FillBoardWithTilesAction(board, tiles));

            return this;
        }

        public IBoardBuilder UseUniformLayout(SquareType type)
        {
            _buildActions.Add(board => ApplyUniformLayoutOnBoardAction(board, type));

            return this;
        }

        public IBoardBuilder UseLayout(SquareType[,] layout)
        {
            if (!DimensionsMatchWithBoard(layout)) throw new ArgumentOutOfRangeException(nameof(layout));

            _buildActions.Add(board => ApplyLayoutOnBoardAction(board, layout));

            return this;
        }

        public IBoardBuilder UseRandomizedLayout(SquareType[] squareTypes, int? seed)
        {
            _buildActions.Add(board => ApplyRandomLayoutOnBoardAction(board, squareTypes, seed));

            return this;
        }

        public IBoardBuilder DisplayTilePoints()
        {
            _buildActions.Add(board => _displayPointsOnBoard = true);

            return this;
        }

        public IBoardBuilder HideTilePoints()
        {
            _buildActions.Add(board => _displayPointsOnBoard = false);

            return this;
        }

        private static void FillBoardWithTilesAction(Square[,] board, Tile[,] tiles)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col].Tile = tiles[row, col];
                }
            }
        }

        private void ApplyUniformLayoutOnBoardAction(Square[,] board, SquareType type)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = new Square(type);
                }
            }
        }

        private static void ApplyRandomLayoutOnBoardAction(Square[,] board, SquareType[] squareTypes, int? seed)
        {
            var emptyLocations = GetAllLocationsOnBoard(board.GetLength(0), board.GetLength(1));
            var rng = seed is null ? new Random() : new Random((int)seed);
            for (int i = 0; i < squareTypes.Length; i++)
            {
                var randomLocationIndex = rng.Next(0, emptyLocations.Count - 1);
                var randomLocation = emptyLocations[randomLocationIndex];
                var squareType = squareTypes[i];

                board[randomLocation.Row, randomLocation.Column] = new Square(squareType);

                emptyLocations.RemoveAt(randomLocationIndex);
            }

            foreach (var emptyLocation in emptyLocations)
            {
                board[emptyLocation.Row, emptyLocation.Column] = new Square(SquareType.Regular);
            }
        }

        private static void ApplyLayoutOnBoardAction(Square[,] board, SquareType[,] layout)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = new Square(layout[row, col]);
                }
            }
        }

        private static List<BoardLocation> GetAllLocationsOnBoard(int rows, int columns)
        {
            var locations = new List<BoardLocation>(rows * columns);
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    locations.Add(new BoardLocation(row, column));
                }
            }
            return locations;
        }

        private bool DimensionsMatchWithBoard<T>(T[,] arr)
        {
            return arr.GetLength(0) == _boardRows && arr.GetLength(1) == _boardColumns;
        }
    }
}
