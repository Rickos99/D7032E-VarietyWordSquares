using System.Collections.Generic;
using System.IO;

namespace Game.Core.Resources
{
    public class DictionaryLoader : IResourceLoader<Dictionary>
    {
        /// <summary>
        /// Load dictionary from file
        /// </summary>
        /// <param name="filepath">Path to file</param>
        /// <returns>The dictionary that was found</returns>
        /// <exception cref="FileLoadException"></exception>
        public Dictionary LoadFromFile(string filepath)
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
