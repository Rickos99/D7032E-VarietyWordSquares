using Game.Core.Resources;
using Game.Util.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Board
{
    public class StandardBoard
    {
        private readonly Square[,] _board;
        private readonly Dictionary _dictionary;
        private readonly bool _ScrabbleModeIsActive;

        public int NumberOfRows => _board.GetLength(0);

        public int NumberOfColumns => _board.GetLength(1);

        /// <summary>
        /// Initialize a new instance of the <see cref="StandardBoard"/> class
        /// with a prefilled board
        /// </summary>
        /// <param name="dictionary">Dictionary to use when calculating board score</param>
        /// <param name="enableScrabbleMode">Indicate if the board is in scrabbleMode</param>
        /// <param name="board">Letters to fill board with</param>
        public StandardBoard(Dictionary dictionary, bool enableScrabbleMode, Square[,] board)
        {
            _board = board;
            _dictionary = dictionary;
            _ScrabbleModeIsActive = enableScrabbleMode;
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="StandardBoard"/> class
        /// with a quadratic board and a width and height of 
        /// <paramref name="dimension"/>.
        /// </summary>
        /// <param name="dictionary">Dictionary to use when calculating board score</param>
        /// <param name="enableScrabbleMode">Indicate if the board is in scrabbleMode</param>
        /// <param name="dimension">Width and height of board</param>
        public StandardBoard(Dictionary dictionary, bool enableScrabbleMode, int dimension)
            : this(dictionary, enableScrabbleMode, dimension, dimension)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="StandardBoard"/> class
        /// with a rectangular board of <paramref name="rowCount"/> rows and 
        /// <paramref name="columnCount"/> columns.
        /// </summary>
        /// <param name="dictionary">Dictionary to use when calculating board score</param>
        /// <param name="enableScrabbleMode">Indicate if the board is in scrabbleMode</param>
        /// <param name="rowCount">Number of rows in board</param>
        /// <param name="columnCount">Number of columns in board</param>
        public StandardBoard(Dictionary dictionary, bool enableScrabbleMode, int rowCount, int columnCount)
        {
            _board = new Square[rowCount, columnCount];
            _dictionary = dictionary;
            _ScrabbleModeIsActive = enableScrabbleMode;

            ReplaceSquaresOnBoard();
        }


        public IEnumerable<KeyValuePair<string, List<Square>>> GetAllWords()
        {
            return _board.GetAllSubEntries(!_ScrabbleModeIsActive)
                .Where((entry) => _dictionary.ContainsWord(entry.Key));
        }

        /// <summary>
        /// Insert a tile on a specified location on board.
        /// </summary>
        /// <param name="location">Location to insert letter at</param>
        /// <param name="tile">Tile to insert</param>
        public void InsertTileAt(BoardLocation location, Tile tile)
        {
            _board[location.Row, location.Column].Tile = tile;
        }

        /// <summary>
        /// Remove a tile on a specified  location on board
        /// </summary>
        /// <param name="location">Location of tile to remove</param>
        public void RemoveTileAt(BoardLocation location)
        {
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
                    var tile = square.Tile;
                    var letter = char.ToUpper(tile.Letter);
                    var points = tile.Points;
                    var str = _ScrabbleModeIsActive ? $"\t{letter} [{points}]" : $"\t  {letter}  ";
                    sb.Append(BoardPainter.PaintSquare(str, square.SquareType));
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }

        private void ReplaceSquaresOnBoard()
        {
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    _board[row, col] = new Square(SquareType.Regular);
                }
            }
        }
    }
}
