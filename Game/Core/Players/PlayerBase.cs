using Game.Core.Board;
using Game.Core.Communication;
using Game.Core.Resources;
using System;
using System.Linq;

namespace Game.Core.Players
{
    // LocalPlayer, use Console to ask question and send message. ctor()
    // NetworkPlayer, use network interface to ask questions. ctor(TcpClient client)
    // BotPlayer, don't print anything, only get input. Need support for game ended message. ctor() or ctor(Obj[] answers)
    public abstract class PlayerBase
    {
        /// <summary>
        /// Player identifier
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="PlayerBase"/> class
        /// </summary>
        public PlayerBase()
        {    
            ID = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if (obj is not PlayerBase player) return false;

            return ID == player.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>
        /// Ask player a question and get the response
        /// </summary>
        /// <param name="question">Question to ask</param>
        /// <returns>Players response to question</returns>
        public abstract string AskQuestion(IQuestion question);

        /// <summary>
        /// Send a message to player
        /// </summary>
        /// <param name="message">Message to send</param>
        public abstract void SendMessage(IMessage message);

        /// <summary>
        /// Prompt player to pick a location for specified tile
        /// </summary>
        /// <param name="tile">Tile to pick a location for</param>
        /// <returns>The location that was picked</returns>
        public virtual BoardLocation PickTileLocation(Tile tile)
        {
            string input = string.Empty;
            bool validInput = false;
            while (!validInput)
            {
                input = AskQuestion(new PickTileLocationQuestion(tile));
                validInput = BoardLocationTranslator.LocationStringIsValid(input);
                if (!validInput)
                {
                    SendMessage(new InformationMessage("Invalid syntax"));
                }
            }
            return BoardLocationTranslator.TranslateFromString(input);
        }

        /// <summary>
        /// Prompt player to pick a tile
        /// </summary>
        /// <param name="tileSchema">Tileschema which contains all the letters</param>
        /// <returns></returns>
        public virtual Tile PickTile(TileSchema tileSchema)
        {
            string input = string.Empty;
            bool validInput = false;
            while (!validInput)
            {
                input = AskQuestion(new PickATileQuestion());
                validInput = input?.Length == 1 && tileSchema.TileExist(input[0]);
                if (!validInput)
                {
                    SendMessage(new InformationMessage("Invalid letter"));
                }
            }
            return tileSchema.Tiles.Where((t) => t.Letter == char.ToLower(input[0])).First();
        }
    }
}
