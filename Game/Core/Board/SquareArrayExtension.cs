using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Board
{
    /// <summary>
    /// Extension for a array of type <see cref="Square"/>.
    /// </summary>
    public static class SquareArrayExtension
    {
        /// <summary>
        /// Get all sub sequences from a two dimensional array of <see cref="Square"/>. The subsequences will be identified left-to-right horizontally, topdown vertically, and forward diagonally and includes duplicates, if any exists.
        /// </summary>
        /// <param name="board">The square two dimensional array to get all subsequences from.</param>
        /// <returns>All subsequences found.</returns>
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

        /// <summary>
        /// Get all entries in an list type <see cref="Square"/> as a string. Each square will contribute to the string construction with its <see cref="Tile.Letter"/> value.
        /// </summary>
        /// <param name="squares">List to get as a string.</param>
        /// <returns>All entries the list as a string.</returns>
        /// <exception cref="NullReferenceException">Thrown when a square in the list does not contain a <see cref="Tile.Letter"/> value.</exception>
        public static string GetEntriesAsString(this List<Square> squares)
        {
            var sb = new StringBuilder();
            foreach (var square in squares)
            {
                sb.Append(square.Tile.Letter);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Make a deep copy of a two dimensional square array.
        /// </summary>
        /// <param name="arr">Array to copy.</param>
        /// <returns>A deep copy of a two dimensional square array</returns>
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
