using Game.Core.Board;
using Game.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core.GameModes
{
    /// <summary>
    /// A collection of players and their boards, the boards can be unique for each player or shared as one instance.
    /// </summary>
    public class PlayerAndBoardCollection
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="PlayerAndBoardCollection"/>
        /// </summary>
        public PlayerAndBoardCollection()
        {
            _collection = new Dictionary<PlayerBase, StandardBoard>();
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="PlayerAndBoardCollection"/>
        /// with a specified inital capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the collection</param>
        public PlayerAndBoardCollection(int capacity)
        {
            _collection = new Dictionary<PlayerBase, StandardBoard>(capacity);
        }

        private readonly Dictionary<PlayerBase, StandardBoard> _collection;

        /// <summary>
        /// Index of last player that was returned by <see cref="GetNextPlayer"/>. If no player has been previously returned, the value will be -1.
        /// </summary>
        private int _lastPlayerIndex = -1;

        /// <summary>
        /// Gets or sets the value for the specified player
        /// </summary>
        /// <param name="player">The player of a board to get or set</param>
        /// <returns>
        /// The board associated with the specified player. If the specified player is
        /// not found, a get operation throws a <see cref="KeyNotFoundException"/>, and a set 
        /// operation associate a board with the specified player.
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public StandardBoard this[PlayerBase player]
        {
            get => _collection[player];
            set => _collection[player] = value;
        }

        /// <summary>
        /// Get all players currently stored in the <see cref="PlayerAndBoardCollection"/>. 
        /// The order of players is the same as the order in which they were added.
        /// </summary>
        public List<PlayerBase> Players
        {
            get => _collection.Keys.ToList();
        }

        /// <summary>
        /// Get all boards currently stored in the <see cref="PlayerAndBoardCollection"/>
        /// </summary>
        public List<StandardBoard> Boards
        {
            get => _collection.Values.ToList();
        }

        /// <summary>
        /// Get the number of elements in the colleciton
        /// </summary>
        public int Count
        {
            get => _collection.Count;
        }

        /// <summary>
        /// Adds the specified player and board to the collection.
        /// </summary>
        /// <param name="player">The board owner to add</param>
        /// <param name="board">The board to add</param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(PlayerBase player, StandardBoard board)
        {
            _collection.Add(player, board);
        }

        /// <summary>
        /// Get the next player from <see cref="Players"/>. On first call, a random player will be returned.
        /// </summary>
        /// <returns>A player</returns>
        public PlayerBase GetNextPlayer()
        {
            int nextPlayerIndex = _lastPlayerIndex == -1
                ? new Random().Next(0, Players.Count)
                : ++_lastPlayerIndex % (Players.Count);

            _lastPlayerIndex = nextPlayerIndex;
            return Players[nextPlayerIndex];
        }
    }
}
