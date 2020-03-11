using System;
using System.Collections.Generic;
using System.IO;
using Sudoku.Model;

namespace Sudoku.Control
{
    /// <summary>
    ///     A player loader.
    /// </summary>
    public class PlayerLoader : IPlayerLoader
    {
        /// <inheritdoc/>
        public void SavePlayer(Player player)
        {
            using var fs = new FileStream(Path.Combine("player", player.ID.ToString()), FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(fs);
            player.Save(writer);
        }

        /// <inheritdoc/>
        public IEnumerable<Player> LoadPlayers()
        {
            DirectoryInfo d = new DirectoryInfo("player");
            d.Create();
            foreach (var file in d.GetFiles())      
            {
                using var fs = file.Open(FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(fs);
                yield return new Player(reader);
            }
        }
    }
}
