namespace Game.Core.Board
{
    public class Tile
    {
        /// <summary>
        /// Get lowercase letter
        /// </summary>
        public char Letter { get; private set; }

        /// <summary>
        /// Get the points of tile
        /// </summary>
        public int Points { get; private set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="Tile"/> class with a 
        /// letter and its point value.
        /// </summary>
        /// <param name="letter">Letter on tile</param>
        /// <param name="points">Point value of <see cref="Tile"/></param>
        public Tile(char letter, int points)
        {
            Letter = char.ToLower(letter);
            Points = points;
        }
    }
}
