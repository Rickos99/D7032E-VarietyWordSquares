namespace Game.Core.Board
{
    public class Square
    {
        public SquareType SquareType { get; private set; }
        public Tile Tile { get; set; }
        public bool IsEmpty { get => Tile == null; }

        public Square(SquareType squareType, Tile tile)
        {
            SquareType = squareType;
            Tile = tile;
        }
    }
}
