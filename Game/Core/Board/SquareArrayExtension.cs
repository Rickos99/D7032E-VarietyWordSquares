using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Board
{
    public static class SquareArrayExtension
    {
        public static IEnumerable<List<Square>> GetAllSubSequences(this Square[,] board)
        {
            var subEntriesFound = new List<List<Square>>();

            subEntriesFound.AddRange(GetSubEntriesInRows());
            subEntriesFound.AddRange(GetSubEntriesInColumns());
            subEntriesFound.AddRange(GetSubEntriesInForwardDiagonals());

            return subEntriesFound;

            List<List<Square>> GetSubEntriesInRows()
            {
                var rows = new List<List<Square>>();
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    var rowEntries = new List<Square>(board.GetLength(0));
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        rowEntries.Add(board[row, col]);
                    }
                    rows.AddRange(GetSubEntries(rowEntries));
                }
                return rows;
            }

            List<List<Square>> GetSubEntriesInColumns()
            {
                var columns = new List<List<Square>>();
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    var colEntries = new List<Square>(board.GetLength(0));
                    for (int row = 0; row < board.GetLength(0); row++)
                    {
                        colEntries.Add(board[row, col]);
                    }
                    columns.AddRange(GetSubEntries(colEntries));
                }
                return columns;
            }

            List<List<Square>> GetSubEntriesInForwardDiagonals()
            {
                var diagonals = new List<List<Square>>();
                // Find all forward diagnonals while moving along first row
                for (int boardCol = board.GetLength(1) - 1; boardCol > 0; boardCol--)
                    diagonals.AddRange(GetSubEntriesInForwardDiagonal(0, boardCol));

                // Find all forward diagonals while moving along first column
                for (int boardRow = 0; boardRow < board.GetLength(0); boardRow++)
                    diagonals.AddRange(GetSubEntriesInForwardDiagonal(boardRow, 0));

                return diagonals;
            }

            List<List<Square>> GetSubEntriesInForwardDiagonal(int row, int col)
            {
                int rowLimit = board.GetLength(0);
                int colLimit = board.GetLength(1);

                var diagEntries = new List<Square>();
                do
                {
                    diagEntries.Add(board[row, col]);
                } while (++row < rowLimit && ++col < colLimit);

                return GetSubEntries(diagEntries);
            }

            List<List<Square>> GetSubEntries(List<Square> entries)
            {
                var subEntries = new List<List<Square>>();
                for (int i = 0; i < entries.Count; i++)
                {
                    for (int j = i + 1; j <= entries.Count; j++)
                    {
                        List<Square> subEntry = entries.GetRange(i, j - i);
                        subEntries.Add(subEntry);
                    }
                }
                return subEntries;
            }
        }

        public static string GetEntriesAsString(this List<Square> squares)
        {
            var sb = new StringBuilder();
            foreach (var square in squares)
            {
                sb.Append(square.Tile.Letter);
            }
            return sb.ToString();
        }

        public static Square[,] DeepCopy(this Square[,] arr)
        {
            int numberOfRows = arr.GetLength(0);
            int numberOfColumns = arr.GetLength(1);

            var arrCopy = new Square[numberOfRows, numberOfColumns];
            for (int row = 0; row < numberOfRows; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    var originalSquare = arr[row, column];
                    var originalSquareType = originalSquare.SquareType;
                    var originalTile = originalSquare.Tile;

                    Tile tileCopy = null;
                    if (!originalSquare.IsEmpty)
                    {
                        tileCopy = new Tile(originalTile.Letter, originalTile.Points);
                    }
                    var squareCopy = new Square(originalSquareType, tileCopy);

                    arrCopy[row, column] = squareCopy;
                }
            }

            return arrCopy;
        }

        /// <summary>
        /// Get a list of distinct squaretypes, from lowest to highest valued squaretype.
        /// </summary>
        /// <returns>A list of distinct squaretypes, from lowest to highest valued squaretype.</returns>
        public static List<SquareType> GetAllDistinctSquareTypes(this Square[,] arr)
        {
            var squareTypeRepresentationOfArr = new List<SquareType>();

            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    squareTypeRepresentationOfArr.Add(arr[row, col].SquareType);
                }
            }

            return squareTypeRepresentationOfArr.Distinct()
                .OrderByDescending(squareType => (int)SquareType.TrippleWord)
                .ToList();
        }
    }
}
