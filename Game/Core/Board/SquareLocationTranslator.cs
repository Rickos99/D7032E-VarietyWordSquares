using System;

namespace Game.Core.Board
{
    public static class SquareLocationTranslator
    {
        /// <summary>
        /// Translate numerical row and column values to a string value on form "[A-Z][0-9]" 
        /// where the first part "[A-Z]" and the second part "[0-9]" represents the row and 
        /// column, respectively . The location is zero-based, i.e. row 0 corresponds to 
        /// row "A".
        /// 
        /// <code>row=1 and column=5 returns "B5"</code>
        /// 
        /// </summary>
        /// <param name="row">Zero index row number</param>
        /// <param name="column">Zero index columns number</param>
        /// <returns>A location string on form "[A-Z][0-9]"</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string TranslateToString(int row, int column)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0) throw new ArgumentOutOfRangeException(nameof(column));

            return IntegerToAlphabetic(row) + column;
        }

        /// <summary>
        /// Translate a location string into a numerical <see cref="SquareLocation"/>-class.
        /// The location string is on the form "[A-Z][0-9]", where the first part "[A-Z]" and 
        /// the second part "[0-9]" represents the row and column, respectively . The location
        /// is zero-based, i.e. row "A" corresponds to row 0.
        /// 
        /// <code>"B5" translate into row=1 and column=5</code>
        /// 
        /// </summary>
        /// <param name="location">A location string to translate</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public static SquareLocation TranslateFromString(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentException("Argument cannot be null or whitespace", nameof(location));
            }
            if (!LocationStringIsValid(location))
            {
                throw new FormatException("Location string can only contain letters A-Z and digits 0-9. Allowed format is '[A-Z]+[0-9]+'");
            }

            location = location.ToUpper();
            string rowStr = string.Empty;
            string colStr = string.Empty;
            for (int i = 0; i < location.Length; i++)
            {
                if (char.IsLetter(location[i])) rowStr += location[i];
                else colStr += location[i];
            }

            int row = AlphabeticToInteger(rowStr);
            int col = int.Parse(colStr);

            return new SquareLocation(row, col);
        }

        /// <summary>
        /// Check whether the provided location string is valid. Allowed format is "[A-Z]+[0-9]+".
        /// </summary>
        /// <param name="location">Location string to check</param>
        /// <returns><c>true</c> if the location string is valid; Otherwise <c>false</c>.</returns>
        public static bool LocationStringIsValid(string location)
        {
            if (!char.IsDigit(location[^1])) return false;

            bool isLookingForLetter = true;
            for (int i = 0; i < location.Length; i++)
            {
                char character = location[i];
                if (isLookingForLetter)
                {
                    if (!char.IsLetterOrDigit(character)) return false;
                    if (char.IsDigit(character)) isLookingForLetter = false;
                }
                else
                {
                    if (!char.IsDigit(character)) return false;
                }
            }
            return true;
        }

        private static string IntegerToAlphabetic(int integer)
        {
            if (integer < 0) throw new ArgumentOutOfRangeException(nameof(integer));

            int lettersInAlphabet = 26;
            int quot = integer / lettersInAlphabet;
            int rem = integer % lettersInAlphabet;
            char letter = (char)('A' + rem);
            if (quot == 0)
            {
                return letter.ToString();
            }
            else
            {
                return IntegerToAlphabetic(quot - 1) + letter;
            }
        }

        private static int AlphabeticToInteger(string str)
        {
            int result = 0;

            int lettersInAlphabet = 26;
            for (int i = 0; i < str.Length; i++)
            {
                int indexInAlphabet = str[i] - 'A' + 1;
                int exponent = str.Length - 1 - i;
                result += Convert.ToInt32(Math.Pow(lettersInAlphabet, exponent) * indexInAlphabet);
            }

            return result - 1; // Convert from one-based to zero-based indexing
        }
    }
}
