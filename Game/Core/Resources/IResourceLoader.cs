namespace Game.Core.Resources
{
    interface IResourceLoader<T>
    {
        T LoadFromFile(string filepath);
    }
}
