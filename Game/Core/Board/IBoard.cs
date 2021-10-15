using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Board
{
    public interface IBoard
    {
        /// <summary>
        /// Get all avaliable letters
        /// </summary>
        public Tile[] AvaliableLetters { get; }

        /// <summary>
        /// Get number of rows on board
        /// </summary>
        public int NumberOfRows { get; }

        /// <summary>
        /// Get number of columns on board
        /// </summary>
        public int NumberOfColumns { get; }

        /// <summary>
        /// Get a boolean indicating whether all <see cref="Squares"/> on the 
        /// <see cref="IBoard"/> are filled
        /// </summary>
        /// <returns>True of all squares in the board are filled; else false</returns>
        bool BoardIsFilled();

        /// <summary>
        /// Insert a tile on a specified row and column on board.
        /// </summary>
        /// <param name="row">Boardrow to insert the tile on</param>
        /// <param name="column">Boardcolumn to insert the tile on</param>
        /// <param name="letter">Tile to insert</param>
        void InsertLetterAt(int row, int column, char letter);

        /// <summary>
        /// Get all words
        /// </summary>
        /// <returns></returns>
        string[] GetAllWords();

        /// <summary>
        /// Calculate score of all existing words on board.
        /// </summary>
        /// <returns>Score of all existing words on board</returns>
        int CalculateScore();

        /// <summary>
        /// Get all locations that are not occupied on board as location strings
        /// </summary>
        /// <returns>Location strings of empty locations on board</returns>
        string[] GetAllEmptyLocations();
    }
}
