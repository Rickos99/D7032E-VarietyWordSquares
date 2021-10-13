namespace Game.Core.Board
{
    public class Tile
    {
        public char Letter { get; private set; }
        public int TilePoint { get; private set; }

        public Tile(char letter, int tilePoint)
        {
            Letter = letter;
            TilePoint = tilePoint;
        }
    }
}
