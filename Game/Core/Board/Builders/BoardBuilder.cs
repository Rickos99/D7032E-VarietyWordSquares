namespace Game.Core.Board.Builders
{
    public static class BoardBuilder
    {
        public static IBoardBuilder CreateStandardBuilder(int rows, int columns)
        {
            return new StandardBoardBuilder(rows, columns);
        }
    }
}
