using System.IO;
using Sudoku.Model;

namespace Sudoku.Control
{
    /// <summary>
    ///     A game loader.
    /// </summary>
    class GameLoader : IGameLoader
    {
        /// <summary>
        ///     Saves the given game.
        /// </summary>
        /// <param name="game"> [out] The game. </param>
        public void Save(Game game)
        {
            if (!HasSaveFile(game.Player, out FileInfo saveFile))
            {
                saveFile.Directory.Create();
            }
            using var s = saveFile.Open(FileMode.Create, FileAccess.Write);
            using var w = new BinaryWriter(s);
            w.Write(game.Time);
            w.Write(game.Score);
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    var cell = game.Get(x, y);
                    w.Write(cell.Value);
                    w.Write(cell.Locked);
                    w.Write(cell.ScoreState);
                }
        }

        /// <summary>
        ///     Loads.
        /// </summary>
        /// <param name="player"> The player. </param>
        /// <param name="game">   [out] The game. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        public bool Load(Player player, out Game game)
        {
            game = null;
            if (!HasSaveFile(player, out FileInfo saveFile)) return false;
            using var s = saveFile.Open(FileMode.Open, FileAccess.Read);
            using var r = new BinaryReader(s);
            game = new Game(player, r.ReadInt32(), r.ReadInt32());
            game.Load();
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    game.Set(x, y, new Cell
                    {
                        Value = r.ReadByte(),
                        Locked = r.ReadInt32(),
                        ScoreState = r.ReadByte()
                    });
                }
            return true;
        }

        /// <summary>
        ///     Query if 'player' has save file.
        /// </summary>
        /// <param name="player">   The player. </param>
        /// <param name="saveFile"> [out] The save file. </param>
        /// <returns>
        ///     True if save file, false if not.
        /// </returns>
        public bool HasSaveFile(Player player, out FileInfo saveFile)
        {
            saveFile = new FileInfo(Path.Combine("savegames", player.ID.ToString()));
            return saveFile.Exists;
        }
    }
}
