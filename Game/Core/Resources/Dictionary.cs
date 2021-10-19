using Game.Util.DataStructures.StringSearch;
using System.Collections.Generic;
using System.IO;
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

        private readonly Trie _words;

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
            _words = new Trie();
            _words.InsertRange(words
                .Where((w)=> !string.IsNullOrEmpty(w))
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
        /// Load dictionary from file
        /// </summary>
        /// <param name="filepath">Path to file</param>
        /// <returns>The dictionary that was found</returns>
        /// <exception cref="FileLoadException"></exception>
        public static Dictionary LoadFromFile(string filepath)
        {
            string name = Path.GetFileNameWithoutExtension(filepath);
            IEnumerable<string> words;
            try
            {
                words = File.ReadLines(filepath);
            }
            catch
            {
                throw new FileLoadException("The specified dictionary could not be loaded", filepath);
            }
            return new Dictionary(name, words);
        }
    }
}
