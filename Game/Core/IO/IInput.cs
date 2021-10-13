using System;

namespace Game.Core.IO
{
    /// <summary>
    /// Interface for application to get input from user
    /// </summary>
    public interface IInput
    {
        /// <summary>
        /// Get text input from user.
        /// </summary>
        /// <returns>Text entered by user</returns>
        string GetInput();

        /// <summary>
        /// Get text input from user and ensure the text fulfills the requirements specified in validator.
        /// </summary>
        /// <param name="validator">Validator to use to validate the user text input</param>
        /// <returns>The text input from user that has passed the validator</returns>
        string GetInput(Predicate<string> validator);
    }
}
