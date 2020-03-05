using System.IO;
using Sudoku.Model;

namespace Sudoku.Control
{
    class GameLoader : IGameLoader
    {
        public void Save(Game game)
        {
            if (!HasSaveFile(game.Player, out var saveFile))
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

        public bool Load(Player player, out Game game)
        {
            game = null;
            if (!HasSaveFile(player, out var saveFile)) return false;
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

        public bool HasSaveFile(Player player, out FileInfo saveFile)
        {
            saveFile = new FileInfo(Path.Combine("savegames", player.ID.ToString()));
            return saveFile.Exists;
        }
    }
}
