using Game.Core.Board;
using Game.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.GameModes
{
    interface IGameMode
    {
        /// <summary>
        /// A list of players that are active in the gamemode.
        /// </summary>
        IList<PlayerBase> Players { get; }

        /// <summary>
        /// Contains all players boards.
        /// </summary>
        IDictionary<PlayerBase, IBoard> Boards { get; }

        /// <summary>
        /// Indicates if the game has started.
        /// </summary>
        bool GameHasStarted { get; }

        void Start();

        void UseBoard(IBoard board);
    }
}
