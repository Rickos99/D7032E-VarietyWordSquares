using Game.Core.Board;
using Game.Core.Board.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core.Resources
{
    public sealed class Dictionary
    {
        /// <summary>
        /// Name of dictionary
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Number of words in dictionary
        /// </summary>
        public int WordCount { get; private set; }

        private readonly SquareTrie _words;

        /// <summary>
        /// Initialize a new instance of the <see cref="Dictionary"/> class with 
        /// a spceified list of words and a user friendly name of the dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="words"></param>
        public Dictionary(string name, IEnumerable<string> words)
        {
            Name = name;
            WordCount = words.Count();
            _words = new SquareTrie();
            _words.InsertRange(words
                .Where((w) => !string.IsNullOrEmpty(w))
                .Select((w) => w.ToLower()));
        }

        /// <summary>
        /// Check whether the dictionary contains a specific word
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns><c>true</c> if dictionary contains <paramref name="word"/>; Otherwise <c>false</c></returns>
        public bool ContainsWord(string word)
        {
            return _words.Search(word.ToLower());
        }

        /// <summary>
        /// Check whether the dictionary contains a specific square sequence
        /// </summary>
        /// <param name="sequence">Square sequence to check</param>
        /// <returns><c>true</c> if dictionary contains <paramref name="sequence"/>; Otherwise <c>false</c></returns>
        public bool ContainsSquareSequence(List<Square> sequence)
        {
            return _words.Search(sequence);
        }
    }
}
