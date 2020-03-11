using System.IO;
using Sudoku.Model;

namespace Sudoku.Control
{
    /// <summary>
    ///     Interface for game loader.
    /// </summary>
    public interface IGameLoader
    {
        /// <summary>
        ///     Saves the given game.
        /// </summary>
        /// <param name="game"> [out] The game. </param>
        void Save(Game game);

        /// <summary>
        ///     Loads.
        /// </summary>
        /// <param name="player"> The player. </param>
        /// <param name="game">   [out] The game. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        bool Load(Player player, out Game game);

        /// <summary>
        ///     Query if 'player' has save file.
        /// </summary>
        /// <param name="player">   The player. </param>
        /// <param name="saveFile"> [out] The save file. </param>
        /// <returns>
        ///     True if save file, false if not.
        /// </returns>
        bool HasSaveFile(Player player, out FileInfo saveFile);
    }
}