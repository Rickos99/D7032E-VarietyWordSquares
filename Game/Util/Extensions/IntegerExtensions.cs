using System;

namespace Game.Util.Extensions
{
    /// <summary>
    /// Extension methods for integers
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Convert a positive integer to a string containing A-Z.
        /// <para>
        /// Example: <br></br>
        ///     39 => "AN" <br></br>
        ///     26 => "AA" <br></br>
        ///     0 => "A"
        /// </para>
        /// </summary>
        /// <param name="integer">Integer to convert</param>
        /// <returns>Converted integer as string</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string ConvertToAlphabetic(this int integer)
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
                return (quot - 1).ConvertToAlphabetic() + letter;
            }
        }
    }
}
