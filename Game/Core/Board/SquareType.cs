using System.ComponentModel;

namespace Game.Core.Board
{
    public enum SquareType
    {
        [Description("STD")]
        Regular,

        [Description("DL")]
        DoubleLetter,

        [Description("TL")]
        TripleLetter,

        [Description("DW")]
        DoubleWord,

        [Description("TW")]
        TrippleWord
    }
}
