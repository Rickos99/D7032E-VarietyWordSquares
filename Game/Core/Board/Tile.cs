namespace Game.Core.Board
{
    public class Tile
    {
        public char Letter { get; private set; }
        public int Points { get; private set; }

        public Tile(char letter, int points)
        {
            Letter = letter;
            Points = points;
        }
    }
}
