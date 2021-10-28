namespace Game.Core.Resources
{
    interface IResourceLoader<T>
    {
        /// <summary>
        /// Load a resource from the specified filepath.
        /// </summary>
        /// <param name="filepath">Path to file</param>
        /// <returns>A resource from the specified filepath.</returns>
        T LoadFromFile(string filepath);
    }
}
