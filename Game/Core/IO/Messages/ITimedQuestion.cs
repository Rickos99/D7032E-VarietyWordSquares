namespace Game.Core.IO.Messages
{
    interface ITimedQuestion : IQuestion
    {
        /// <summary>
        /// Time to answer the question
        /// </summary>
        int SecondsTimeout { get; }
    }
}
