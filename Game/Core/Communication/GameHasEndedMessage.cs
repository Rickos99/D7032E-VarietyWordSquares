namespace Game.Core.Communication
{
    /// <summary>
    /// A message used when a game has ended and to tell a network client to disconnect.
    /// </summary>
    public class GameHasEndedMessage : IMessage
    {
        public string Content => "Game has been ended by host. \n\n Thanks for playing!";

        public string GetMessageString()
        {
            return Content;
        }
    }
}
