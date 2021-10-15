using System.Collections.Generic;

namespace Game.Util.DataStructures.StringSearch
{
    public class Trie
    {
        private readonly Node _root;

        /// <summary>
        /// Initialize a new instance of <see cref="Trie"/> class
        /// </summary>
        public Trie()
        {
            _root = new Node('^', 0, null);
        }

        public Node Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        /// <summary>
        /// Check whether a string exists in <see cref="Trie"/>
        /// </summary>
        /// <param name="s">String to search for</param>
        /// <returns><c>true</c> if the string is found; Otherwise false</returns>
        public bool Search(string s)
        {
            var prefix = Prefix(s);
            return prefix.Depth == s.Length && prefix.FindChildNode('$') != null;
        }

        /// <summary>
        /// Insert a range of strings
        /// </summary>
        /// <param name="items">A list of string to insert</param>
        public void InsertRange(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
                Insert(items[i]);
        }

        /// <summary>
        /// Insert a string
        /// </summary>
        /// <param name="s">String to insert</param>
        public void Insert(string s)
        {
            var commonPrefix = Prefix(s);
            var current = commonPrefix;

            for (var i = current.Depth; i < s.Length; i++)
            {
                var newNode = new Node(s[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }

            current.Children.Add(new Node('$', current.Depth + 1, current));
        }

        /// <summary>
        /// Delete a string
        /// </summary>
        /// <param name="s">String to delete</param>
        public void Delete(string s)
        {
            if (Search(s))
            {
                var node = Prefix(s).FindChildNode('$');

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
