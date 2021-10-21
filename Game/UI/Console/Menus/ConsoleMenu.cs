using Game.UI.Console.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UI.Console.Menus
{
    public class ConsoleMenu : IControl
    {
        /// <summary>
        /// String to be used before and after the menu has been printed.
        /// </summary>
        public string Seperator { get; set; }
            = "****************************************************************";

        /// <summary>
        /// Menu header
        /// </summary>
        public string Header { get; private set; }

        /// <summary>
        /// Choices the menu should have
        /// </summary>
        public IList<MenuChoice> MenuChoices { get; private set; }

        /// <summary>
        /// Indicated whether the menu has a header
        /// </summary>
        /// <value><c>true</c> if the menu has an non-empty header; Otherwise <c>false</c></value>
        public bool HasHeader { get => string.IsNullOrWhiteSpace(Header); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="menuChoices"></param>
        public ConsoleMenu(string header, IList<MenuChoice> menuChoices)
        {
            Header = header;
            MenuChoices = menuChoices;
        }

        /// <summary>
        /// Print menu in console, let user select an choice and invoke its action.
        /// </summary>
        public virtual void Show()
        {
            while (true)
            {
                System.Console.WriteLine(ToString());
                var choice = LetUserSelectChoice();
                if (choice is ExitMenuChoice) break;
                choice.Action();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Seperator);
            if (!string.IsNullOrWhiteSpace(Header))
            {
                sb.AppendLine($" {Header}");
            }

            foreach (var choice in MenuChoices)
            {
                sb.AppendLine($"  [{choice.Selector}] {choice.Description}");
            }
            sb.AppendLine(Seperator);

            return sb.ToString();
        }


        private MenuChoice LetUserSelectChoice()
        {
            var input = string.Empty;
            do
            {
                if (string.IsNullOrEmpty(input) == false) System.Console.WriteLine("Invalid option");
                System.Console.Write("Select an option: ");
                input = System.Console.ReadLine();
            } while (!MenuChoicesHasSelector(input));

            return MenuChoices.Where(choice => choice.Selector.ToUpper() == input.ToUpper()).FirstOrDefault();
        }

        private bool MenuChoicesHasSelector(string selector)
        {
            return MenuChoices.Any(choice => choice.Selector.ToUpper() == selector.ToUpper());
        }
    }
}
