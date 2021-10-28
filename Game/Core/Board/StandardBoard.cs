using Game.Core.Exceptions;
using Game.Util.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Game.Core.Board
{
    public class StandardBoard
    {
        private readonly Square[,] _board;
        private readonly bool _displayPointsOnBoard;
        private readonly List<SquareType> _squareTypesOnBoard;

        public int NumberOfRows => _board.GetLength(0);

        public int NumberOfColumns => _board.GetLength(1);

        /// <summary>
        /// Initialize a new instance of the <see cref="StandardBoard"/> class
        /// with a predefined board
        /// </summary>
        /// <param name="board">Letters to fill board with</param>
        /// <param name="displayPointsOnBoard">Indicate whether to display tile points when getting the board as string</param>
        public StandardBoard(Square[,] board, bool displayPointsOnBoard)
        {
            _board = board.DeepCopy();
            _displayPointsOnBoard = displayPointsOnBoard;
            _squareTypesOnBoard = _board.GetAllDistinctSquareTypes();
        }

        /// <summary>
        /// Get all square sequences, including left to right rows, downwards columns, and forward diagonals.
        /// </summary>
        /// <returns>All square sequences on board</returns>
        public IEnumerable<List<Square>> GetAllSquareSequences()
        {
            return _board.GetAllSubSequences();
        }

        /// <summary>
        /// Insert a tile on a specified location on board.
        /// </summary>
        /// <param name="location">Location to insert letter at</param>
        /// <param name="tile">Tile to insert</param>
        /// <exception cref="LocationOutsideBoardException"></exception>
        public void InsertTileAt(BoardLocation location, Tile tile)
        {
            if (!LocationIsPresentOnBoard(location))
            {
                throw new LocationOutsideBoardException();
            }
            _board[location.Row, location.Column].Tile = tile;
        }

        /// <summary>
        /// Remove a tile on a specified  location on board
        /// </summary>
        /// <param name="location">Location of tile to remove</param>
        /// <exception cref="LocationOutsideBoardException"></exception>
        public void RemoveTileAt(BoardLocation location)
        {
            if (!LocationIsPresentOnBoard(location))
            {
                throw new LocationOutsideBoardException();
            }
            _board[location.Row, location.Column].Tile = null;
        }

        /// <summary>
        /// Get a boolean indicating whether all squares are filled
        /// </summary>
        /// <returns>True if all squares in the board are filled; Otherwise false</returns>
        public bool IsFilled()
        {
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    if (_board[row, col].IsEmpty) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get all locations that are not occupied on board as location strings
        /// </summary>
        /// <returns>Location strings of empty locations on board</returns>
        public IEnumerable<BoardLocation> GetAllEmptyLocations()
        {
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    if (_board[row, col].IsEmpty)
                    {
                        yield return new BoardLocation(row, col);
                    }
                }
            }
        }

        /// <summary>
        /// Check whether the row or column defined in <paramref name="location"/>
        /// is present on <see cref="StandardBoard"/>.
        /// </summary>
        /// <param name="location"></param>
        /// <returns><c>true</c> if location is present on board; Otherwise false</returns>
        public bool LocationIsPresentOnBoard(BoardLocation location)
        {
            return location.Row < _board.GetLength(0) && location.Column < _board.GetLength(1);
        }

        /// <summary>
        /// Check whether a location on board is empty.
        /// </summary>
        /// <param name="location">Location to check</param>
        /// <returns>true<c> if the the location is empty; Otherwise false</c></returns>
        /// <exception cref="LocationOutsideBoardException"></exception>
        public bool LocationIsEmpty(BoardLocation location)
        {
            if (!LocationIsPresentOnBoard(location))
            {
                throw new LocationOutsideBoardException();
            }
            return _board[location.Row, location.Column].IsEmpty;
        }

        /// <summary>
        /// Create a deep copy of this board.
        /// </summary>
        /// <returns>A deep copy of this board.</returns>
        public StandardBoard Copy()
        {
            return new StandardBoard(_board.DeepCopy(), _displayPointsOnBoard);
        }

        /// <summary>
        /// Get current board as a pretty string
        /// </summary>
        /// <remarks>
        /// All letters on board will be in uppercase even if they was inserted as lowercase
        /// </remarks>
        /// <returns>Current board as a pretty string</returns>
        public string GetBoardAsString()
        {
            var sb = new StringBuilder();
            // Print header
            for (int col = 0; col < _board.GetLength(1); col++)
            {
                sb.Append(BoardPainter.PaintHeader($"\t  {col}  "));
            }
            sb.Append('\n');

            // Print rows
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                sb.Append(BoardPainter.PaintHeader($"  {row.ConvertToAlphabetic()}  "));
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    var square = _board[row, col];
                    string tileString = "\t     ";
                    if (!square.IsEmpty)
                    {
                        var tile = square.Tile;
                        var letter = char.ToUpper(tile.Letter);
                        var points = tile.Points;
                        tileString = _displayPointsOnBoard ? $"\t{letter} [{points}]" : $"\t  {letter}  ";
                    }
                    sb.Append(BoardPainter.PaintSquare(tileString, square.SquareType));
                }
                sb.Append('\n');
            }

            // Print squaretypes
            if (_squareTypesOnBoard.Count > 1)
            {
                sb.Append('\n');
                foreach (var squareType in _squareTypesOnBoard)
                {
                    var squareDescription = $"\t {squareType.GetDescription()} ";
                    sb.Append(BoardPainter.PaintSquare(squareDescription, squareType));
                }
            }

            return sb.ToString();
        }
    }
}
