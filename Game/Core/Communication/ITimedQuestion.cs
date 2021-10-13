namespace Game.Core.Communication
{
    interface ITimedQuestion : IQuestion
    {
        /// <summary>
        /// Time to answer the question
        /// </summary>
        int SecondsTimeout { get; }
    }
}
