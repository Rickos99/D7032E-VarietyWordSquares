namespace Game.Core.Board.Builders
{
    /// <summary>
    /// A builder class to build different kinds wordsquare boards.
    /// </summary>
    public static class BoardBuilder
    {
        /// <summary>
        /// Create a new instance of a <see cref="StandardBoardBuilder"/> class.
        /// </summary>
        /// <param name="rows">Number of rows on board.</param>
        /// <param name="columns">NUmber of columns on board</param>
        /// <returns>A new instance of a <see cref="StandardBoardBuilder"/> class.</returns>
        public static IBoardBuilder CreateStandardBuilder(int rows, int columns)
        {
            return new StandardBoardBuilder(rows, columns);
        }
    }
}
