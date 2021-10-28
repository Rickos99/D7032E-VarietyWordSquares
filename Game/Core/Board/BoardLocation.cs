using System;

namespace Game.Core.Board
{
    /// <summary>
    /// Represents a location on a board with a row and column
    /// </summary>
    public class BoardLocation
    {
        /// <summary>
        /// Row of location on a board, Zero based indexing.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Column of location on a board. Zero based indexing.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardLocation"/> class with row 
        /// and column specified.
        /// </summary>
        /// <param name="row">Zero indexed row number.</param>
        /// <param name="column">Zero indexed column number.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public BoardLocation(int row, int column)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0) throw new ArgumentOutOfRangeException(nameof(column));

            Row = row;
            Column = column;
        }

        /// <summary>
        /// Translate numerical row and column values to a string value on form "[A-Z][0-9]" 
        /// where the first part "[A-Z]" and the second part "[0-9]" represents the row and 
        /// column, respectively.
        /// </summary>
        /// <returns>A string on form "[A-Z][0-9]"</returns>
        public override string ToString()
        {
            return BoardLocationTranslator.TranslateToString(Row, Column);
        }
    }
}
