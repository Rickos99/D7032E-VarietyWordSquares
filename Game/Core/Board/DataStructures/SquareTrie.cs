using Game.Util.DataStructures.StringSearch;
using System.Collections.Generic;

namespace Game.Core.Board.DataStructures
{
    public class SquareTrie : Trie
    {
        /// <summary>
        /// Initialize a new instance of <see cref="SquareTrie"/> class
        /// </summary>
        public SquareTrie() : base()
        {
        }

        public Node Prefix(List<Square> squares)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var square in squares)
            {
                currentNode = currentNode.FindChildNode(square.Tile.Letter);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        /// <summary>
        /// Check whether a square sequence exists in <see cref="Trie"/>
        /// </summary>
        /// <param name="squares">Square sequence to search for</param>
        /// <returns><c>true</c> if the square sequence is found; Otherwise false</returns>
        public bool Search(List<Square> squares)
        {
            var prefix = Prefix(squares);
            return prefix.Depth == squares.Count && prefix.FindChildNode('$') != null;
        }

        /// <summary>
        /// Insert a collection of square sequences
        /// </summary>
        /// <param name="items">A collection of square sequences to insert</param>
        public void InsertRange(IEnumerable<List<Square>> items)
        {
            foreach (var item in items)
                Insert(item);
        }

        /// <summary>
        /// Insert a square sequence
        /// </summary>
        /// <param name="squares">Square sequence to insert</param>
        public void Insert(List<Square> squares)
        {
            var commonPrefix = Prefix(squares);
            var current = commonPrefix;

            for (var i = current.Depth; i < squares.Count; i++)
            {
                var newNode = new Node(squares[i].Tile.Letter, current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }

            current.Children.Add(new Node('$', current.Depth + 1, current));
        }

        /// <summary>
        /// Delete a square sequence
        /// </summary>
        /// <param name="squares">Square sequence to delete</param>
        public void Delete(List<Square> squares)
        {
            if (Search(squares))
            {
                var node = Prefix(squares).FindChildNode('$');

                while (node.IsLeaf())
                {
                    var parent = node.Parent;
                    parent.DeleteChildNode(node.Value);
                    node = parent;
                }
            }
        }
    }
}
