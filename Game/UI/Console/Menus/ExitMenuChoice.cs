namespace Game.UI.Console.Menus
{
    public class ExitMenuChoice : MenuChoice
    {
        public ExitMenuChoice(string description) : base("!", description, () => { })
        {
        }
    }
}
