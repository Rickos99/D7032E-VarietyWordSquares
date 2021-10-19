using Pastel;

namespace Game.Core.Board
{
    public static class BoardPainter
    {
        private const string COLOR_HEADER_BG = "#2472C8";
        private const string COLOR_SQUARE_REGULAR_BG = "#E5E5E5";
        private const string COLOR_SQUARE_DOUBLE_LETTER_BG = "#11A8CD";
        private const string COLOR_SQUARE_TRIPPLE_LETTER_BG = "#0DBC79";
        private const string COLOR_SQUARE_DOUBLE_WORD_BG = "#E5E510";
        private const string COLOR_SQUARE_TRIPPLE_WORD_BG = "#BC3FBC";

        private const string COLOR_LIGHT_TEXT = "#E5E5E5";
        private const string COLOR_DARK_TEXT = "#1E1E1E";

        private const string COLOR_HEADER_TEXT = COLOR_LIGHT_TEXT;
        private const string COLOR_SQUARE_REGULAR_TEXT = COLOR_DARK_TEXT;
        private const string COLOR_SQUARE_DOUBLE_LETTER_TEXT = COLOR_DARK_TEXT;
        private const string COLOR_SQUARE_TRIPPLE_LETTER_TEXT = COLOR_DARK_TEXT;
        private const string COLOR_SQUARE_DOUBLE_WORD_TEXT = COLOR_DARK_TEXT;
        private const string COLOR_SQUARE_TRIPPLE_WORD_TEXT = COLOR_DARK_TEXT;

        public static string PaintHeader(string text)
        {
            return text.Pastel(COLOR_HEADER_TEXT).PastelBg(COLOR_HEADER_BG);
        }

        public static string PaintSquare(string text, SquareType squareType)
        {
            var color = PickColorScheme(squareType);
            return text.Pastel(color.text).PastelBg(color.background);
        }

        private static (string background, string text) PickColorScheme(SquareType squareType)
        {
            switch (squareType)
            {
                case SquareType.Regular:
                    return (COLOR_SQUARE_REGULAR_BG, COLOR_SQUARE_REGULAR_TEXT);
                case SquareType.DoubleLetter:
                    return (COLOR_SQUARE_DOUBLE_LETTER_BG, COLOR_SQUARE_DOUBLE_LETTER_TEXT);
                case SquareType.TripleLetter:
                    return (COLOR_SQUARE_TRIPPLE_LETTER_BG, COLOR_SQUARE_TRIPPLE_LETTER_TEXT);
                case SquareType.DoubleWord:
                    return (COLOR_SQUARE_DOUBLE_WORD_BG, COLOR_SQUARE_DOUBLE_WORD_TEXT);
                case SquareType.TrippleWord:
                    return (COLOR_SQUARE_TRIPPLE_WORD_BG, COLOR_SQUARE_TRIPPLE_WORD_TEXT);
            }

            return (string.Empty, string.Empty);
        }
    }
}
