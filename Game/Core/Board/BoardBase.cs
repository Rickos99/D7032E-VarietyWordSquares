using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Board
{
    public abstract class BoardBase
    {
        public Square[,] Squares { get; private set; }
        public Tile[] AvaliableLetters { get; private set; }
        public virtual int NumberOfRows { get => Squares.GetLength(0); }
        public virtual int NumberOfColumns { get => Squares.GetLength(1); }

        //public Square this[string location] { get => GetContentAt(location); }
        //public Square this[int row, int column] { get => GetContentAt(row, column); }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardBase"/> with
        /// specified .
        /// </summary>
        /// <param name="dimension"></param>
        public BoardBase(int dimension) : this(dimension, dimension) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardBase"/> with
        /// specified number of rows and columns
        /// </summary>
        /// <param name="numberOfRows"></param>
        /// <param name="numberOfColumns"></param>
        public BoardBase(int numberOfRows, int numberOfColumns)
        {
            Squares = new Square[numberOfRows, numberOfColumns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardBase"/> with 
        /// predefined squares.
        /// </summary>
        /// <param name="squares">A 2D array of predefined squares</param>
        public BoardBase(Square[,] squares)
        {
            Squares = squares;
        }

        /// <summary>
        /// Get a boolean indicating whether all <see cref="Squares"/> in the 
        /// <see cref="BoardBase"/> are filled
        /// </summary>
        /// <returns>True of all squares in the board are filled; else false</returns>
        public bool BoardIsFilled()
        {
            for (int row = 0; row < Squares.GetLength(0); row++)
            {
                for (int col = 0; col < Squares.GetLength(1); col++)
                {
                    if (Squares[row, col].IsEmpty) return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// Insert a tile on a specified row and column in board.
        /// </summary>
        /// <param name="row">Boardrow to insert the tile on</param>
        /// <param name="column">Boardcolumn to insert the tile on</param>
        /// <param name="tile">Tile to insert</param>
        public virtual void InsertTileAt(int row, int column, Tile tile)
        {
            Squares[row, column].Tile = tile;
        }

        public abstract string[] GetAllWords();

        // Maybe this should not be located in abstract class, but rather in derived classes
        public abstract int CalculateScore();

        public abstract string[] GetAllEmptyLocations();
    }
}
