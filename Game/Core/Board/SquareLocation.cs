﻿using System;

namespace Game.Core.Board
{
    public class SquareLocation
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareLocation"/> class with row 
        /// and column specified.
        /// </summary>
        /// <param name="row">Zero indexed row number</param>
        /// <param name="column">Zero indexed column number</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public SquareLocation(int row, int column)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0) throw new ArgumentOutOfRangeException(nameof(column));

            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return SquareLocationTranslator.TranslateToString(Row, Column);
        }
    }
}
