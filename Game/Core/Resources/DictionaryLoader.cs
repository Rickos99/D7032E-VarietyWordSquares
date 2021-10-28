using Game.Core.Language;
using System.Collections.Generic;
using System.IO;

namespace Game.Core.Resources
{
    /// <summary>
    /// Load dictionaries from filesystem.
    /// </summary>
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
            if (!Path.IsPathRooted(filepath))
            {
                filepath = Path.Combine(Settings.AssemblyDirectory, filepath);
            }
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
