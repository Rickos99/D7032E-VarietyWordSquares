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

        private Trie _words = null;

        private static Dictionary instance = null;
        private static readonly object padlock = new object();

        private Dictionary()
        {
            _words = new Trie();
            WordCount = 0;
            Name = string.Empty;
        }

        /// <summary>
        /// Get instance of <see cref="Dictionary"/> class
        /// </summary>
        public static Dictionary Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Dictionary();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Replace current directory with a new range of words and a new name.
        /// </summary>
        /// <param name="name">Name of dictionary</param>
        /// <param name="words">Words that the dictionary contains</param>
        public void SetDictionary(string name, List<string> words)
        {
            Name = name;
            WordCount = words.Count;
            _words = new Trie();
            _words.InsertRange(words.Select((w) => w.ToLower()).ToList());
        }

        /// <summary>
        /// Check whether a word exist in dictionary
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns><c>true</c> if word exist in dictionary; Otherwise <c>false</c></returns>
        public bool WordExist(string word)
        {
            return _words.Search(word.ToLower());
        }

        /// <summary>
        /// Load dictionary from file and replace current dictionary
        /// </summary>
        /// <param name="filepath">Path to file</param>
        public void LoadFromFile(string filepath)
        {
            IEnumerable<string> lines;
            try
            {
                lines = File.ReadLines(filepath);
            }
            catch
            {
                throw new FileLoadException("The specified dictionary could not be loaded", filepath);
            }

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                _words.Insert(line.ToLower());
            }

            WordCount = lines.Count();
            Name = Path.GetFileNameWithoutExtension(filepath);
        }
    }
}
