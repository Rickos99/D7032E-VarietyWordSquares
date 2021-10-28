namespace Game.Core.Board
{
    /// <summary>
    /// A square use on a board. It may contain a tile.
    /// </summary>
    public class Square
    {
        /// <summary>
        /// Indicate the scrabble point multiplier type
        /// </summary>
        public SquareType SquareType { get; private set; }

        /// <summary>
        /// Tile placed on square
        /// </summary>
        public Tile Tile { get; set; }

        /// <summary>
        /// Indicate if <see cref="Tile"/> is empty.
        /// </summary>
        public bool IsEmpty { get => Tile == null; }

        /// <summary>
        /// Initialize a new instance of the <see cref="Square"/> class with a 
        /// type and no tile.
        /// </summary>
        /// <param name="squareType">Indicate the scrabble point multiplier</param>
        public Square(SquareType squareType) : this(squareType, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Square"/> class with a 
        /// type and a tile.
        /// </summary>
        /// <param name="squareType">Indicate the scrabble point multiplier</param>
        /// <param name="tile">A tile to add to square</param>
        public Square(SquareType squareType, Tile tile)
        {
            SquareType = squareType;
            Tile = tile;
        }
    }
}
