using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Core.Board
{
    internal static class SquareArrayExtension
    {

        public static IEnumerable<KeyValuePair<string, List<Square>>> GetAllSubEntries(this Square[,] board, bool allowDuplicateSubEntries)
        {
            var alreadyFoundSubstrings = new HashSet<string>();
            var subEntriesFound = new List<KeyValuePair<string, List<Square>>>();

            subEntriesFound.AddRange(GetSubEntriesInRows());
            subEntriesFound.AddRange(GetSubEntriesInColumns());
            subEntriesFound.AddRange(GetSubEntriesInForwardDiagonals());

            return subEntriesFound;

            List<KeyValuePair<string, List<Square>>> GetSubEntriesInRows()
            {
                var rows = new List<KeyValuePair<string, List<Square>>>();
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    var rowEntries = new List<Square>(board.GetLength(0));
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        rowEntries.Add(board[row, col]);
                    }
                    rows.AddRange(GetUniqueSubEntries(rowEntries));
                }
                return rows;
            }

            List<KeyValuePair<string, List<Square>>> GetSubEntriesInColumns()
            {
                var columns = new List<KeyValuePair<string, List<Square>>>();
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    var colEntries = new List<Square>(board.GetLength(0));
                    for (int row = 0; row < board.GetLength(0); row++)
                    {
                        colEntries.Add(board[row, col]);
                    }
                    columns.AddRange(GetUniqueSubEntries(colEntries));
                }
                return columns;
            }

            List<KeyValuePair<string, List<Square>>> GetSubEntriesInForwardDiagonals()
            {
                var diagonals = new List<KeyValuePair<string, List<Square>>>();
                // Find all forward diagnonals while moving along first row
                for (int boardCol = board.GetLength(1) - 1; boardCol > 0; boardCol--)
                    diagonals.AddRange(GetUniqueSubEntriesInForwardDiagonal(0, boardCol));

                // Find all forward diagonals while moving along first column
                for (int boardRow = 0; boardRow < board.GetLength(0); boardRow++)
                    diagonals.AddRange(GetUniqueSubEntriesInForwardDiagonal(boardRow, 0));

                return diagonals;
            }

            List<KeyValuePair<string, List<Square>>> GetUniqueSubEntriesInForwardDiagonal(int row, int col)
            {
                int rowLimit = board.GetLength(0);
                int colLimit = board.GetLength(1);

                var diagEntries = new List<Square>();
                do
                {
                    diagEntries.Add(board[row, col]);
                } while (++row < rowLimit && ++col < colLimit);

                return GetUniqueSubEntries(diagEntries);
            }

            List<KeyValuePair<string, List<Square>>> GetUniqueSubEntries(List<Square> entries)
            {
                var subEntries = new List<KeyValuePair<string, List<Square>>>();
                for (int i = 0; i < entries.Count; i++)
                {
                    for (int j = i + 1; j <= entries.Count; j++)
                    {
                        List<Square> subEntry = entries.GetRange(i, j - i);
                        string subEntryStr = GetEntriesAsString(subEntry);

                        if (allowDuplicateSubEntries)
                        {
                            subEntries.Add(new(subEntryStr, subEntry));
                        }
                        else if (!alreadyFoundSubstrings.Contains(subEntryStr))
                        {
                            alreadyFoundSubstrings.Add(subEntryStr);
                            subEntries.Add(new(subEntryStr, subEntry));
                        }
                    }
                }
                return subEntries;
            }

            string GetEntriesAsString(List<Square> entries)
            {
                var sb = new StringBuilder();
                foreach (var entry in entries)
                {
                    sb.Append(entry.Tile.Letter);
                }
                return sb.ToString();
            }
        }
    }
}
