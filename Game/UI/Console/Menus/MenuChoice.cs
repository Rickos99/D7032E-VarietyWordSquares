using System;

namespace Game.UI.Console.Menus
{
    public class MenuChoice
    {
        /// <summary>
        /// Selector used to select the choice
        /// </summary>
        public string Selector { get; private set; }

        /// <summary>
        /// Action the choice can perform when selected
        /// </summary>
        public Action Action { get; private set; }

        /// <summary>
        /// Get a description of the choice
        /// </summary>
        public string Description { get => getDescription == null ? descriptionString : getDescription(); }


        private readonly Func<string> getDescription;
        private readonly string descriptionString;

        /// <summary>
        /// Initialize a new instance of <see cref="MenuChoice"/> class with a dynamic description.
        /// </summary>
        /// <param name="selector">Selector used to select the choice in menus</param>
        /// <param name="description">Function to generate dynamic description</param>
        /// <param name="action">Action to perform for example when the choice is selected in a menu</param>
        public MenuChoice(string selector, Func<string> description, Action action)
        {
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new ArgumentException($"'{nameof(selector)}' cannot be null or empty.", nameof(selector));
            }

            Selector = selector;
            getDescription = description ?? throw new ArgumentNullException(nameof(description));
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Initialize a new instance of <see cref="MenuChoice"/> class with a static description.
        /// </summary>
        /// <param name="selector">Selector used to select the choice in menus</param>
        /// <param name="description">Description of the menu choice</param>
        /// <param name="action">Action to perform for example when the choice is selected in a menu</param>
        public MenuChoice(string selector, string description, Action action)
        {
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new ArgumentException($"'{nameof(selector)}' cannot be null or empty.", nameof(selector));
            }

            Selector = selector;
            descriptionString = description;
            Action = action;
        }
    }
}
