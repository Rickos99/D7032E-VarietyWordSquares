using System;

namespace Game.Core.Board
{
    public class BoardLocation
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardLocation"/> class with row 
        /// and column specified.
        /// </summary>
        /// <param name="row">Zero indexed row number</param>
        /// <param name="column">Zero indexed column number</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public BoardLocation(int row, int column)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0) throw new ArgumentOutOfRangeException(nameof(column));

            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return BoardLocationTranslator.TranslateToString(Row, Column);
        }
    }
}
