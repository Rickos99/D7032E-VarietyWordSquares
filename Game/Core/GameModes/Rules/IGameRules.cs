using Game.Core.Board;

namespace Game.Core.GameModes.Rules
{
    /// <summary>
    /// Used to define a set of rules in a <see cref="AbstractGameMode"/>
    /// </summary>
    interface IGameRules
    {
        /// <summary>
        /// Calculate a score for the specified board.
        /// </summary>
        /// <param name="board">Bord to calculate a score for.</param>
        /// <returns>A score</returns>
        int CalculateScore(StandardBoard board);

        /// <summary>
        /// Check whether the specified board fulfill the gameover condition.
        /// </summary>
        /// <param name="board">Board to check</param>
        /// <returns><c>true</c> if the board fulfill the gameover condition; Otherwise <c>false</c>. </returns>
        bool BoardHasReachedGameOver(StandardBoard board);
    }
}
