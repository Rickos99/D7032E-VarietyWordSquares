using Game.Core.Board;
using Game.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core.GameModes
{
    class PlayerAndBoardCollection
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
        /// Get all players currently stored in the <see cref="PlayerAndBoardCollection"/>
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
    }
}
