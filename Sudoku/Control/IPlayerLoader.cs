using System.Collections.Generic;
using Sudoku.Model;

namespace Sudoku.Control
{
    /// <summary>
    ///     Interface for player loader.
    /// </summary>
    public interface IPlayerLoader
    {
        /// <summary>
        ///     Saves a player.
        /// </summary>
        /// <param name="player"> The player. </param>
        void SavePlayer(Player player);

        /// <summary>
        ///     Enumerates load players in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process load players in this collection.
        /// </returns>
        IEnumerable<Player> LoadPlayers();
    }
}