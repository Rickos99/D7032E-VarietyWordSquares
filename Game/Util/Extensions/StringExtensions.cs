using System;

namespace Game.Util.Extensions
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Try convert string of variable length containing A-Z to an integer.
        /// <para>
        /// Example: <br></br>
        ///     "AN" => 39 <br></br>
        ///     "AA" => 26 <br></br>
        ///     "A" => 0
        /// </para>
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="integer">Integer value of string</param>
        /// <param name="startAtZero">Indicate if <code>"A" => 0</code></param>
        /// <returns><c>true</c> if string could be converted; Otherwise <c>false</c> and <paramref name="integer"/> is 0.</returns>
        public static bool TryConvertToInteger(this string str, out int integer, bool startAtZero = true)
        {
            str = str.ToUpper();
            integer = startAtZero ? -1 : 0; // Set offset
            try
            {
                int lettersInAlphabet = 26;
                for (int i = 0; i < str.Length; i++)
                {
                    int indexInAlphabet = str[i] - 'A' + 1;
                    int exponent = str.Length - 1 - i;
                    integer += Convert.ToInt32(Math.Pow(lettersInAlphabet, exponent) * indexInAlphabet);
                }

                return true;
            }
            catch
            {
                integer = 0;
                return false;
            }
        }
    }
}
