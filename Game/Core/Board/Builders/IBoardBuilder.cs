using System;

namespace Game.Core.Board.Builders
{
    public interface IBoardBuilder
    {
        /// <summary>
        /// Fill the board with tiles.
        /// </summary>
        /// <param name="tiles">Tiles to fill the board with. Must match the dimensions of board</param>
        /// <returns>The same instance of the <see cref="IBoardBuilder"/> for chaining</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="tiles"/> does not match the boardsize
        /// </exception>
        IBoardBuilder FillWith(Tile[,] tiles);

        /// <summary>
        /// Define a specific squaretype to the board. All squares will be filled with the specified type.
        /// </summary>
        /// <param name="type">Type to use on board</param>
        /// <returns>The same instance of the <see cref="IBoardBuilder"/> for chaining</returns>
        IBoardBuilder UseUniformLayout(SquareType type);

        /// <summary>
        /// Define a specific layout to apply to the board.
        /// </summary>
        /// <param name="layout">Layout to apply. Must match the dimensions of board</param>
        /// <returns>The same instance of the <see cref="IBoardBuilder"/> for chaining</returns>
        /// /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="tiles"/> does not match the boardsize
        /// </exception>
        IBoardBuilder UseLayout(SquareType[,] layout);

        /// <summary>
        /// Apply a random layout to the board. The randomized layout will override any already specified layout.
        /// </summary>
        /// <param name="squareTypes">Tile multipliers to place on board.</param>
        /// <param name="seed">
        ///     Seed to use when selecting random locations on board to place the tile multipliers.
        /// </param>
        /// <returns>The same instance of the <see cref="IBoardBuilder"/> for chaining</returns>
        IBoardBuilder UseRandomizedLayout(SquareType[] squareTypes, int? seed);

        /// <summary>
        /// Run the given actions to initialize the board. This can be only be
        /// called once.
        /// </summary>
        /// <returns>An initialized <see cref="StandardBoard"/></returns>
        StandardBoard Build();
    }
}
