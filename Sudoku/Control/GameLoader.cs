using Sudoku.Model;
using System;
using System.IO;
using System.Text;

namespace Sudoku
{
    class GameLoader : IGameLoader
    {
        public void Save(Player player, Field field)
        {
            using var s = File.OpenWrite($".\\{Convert.ToBase64String(Encoding.UTF8.GetBytes(player.Name))}.sud");
            using var w = new BinaryWriter(s);
            for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
            {
                w.Write(field[x, y].Value);
                w.Write(field[x, y].Locked);
            }
        }

        public bool Load(Player player, out Field field)
        {
            field = new Field();
            if (!HasSaveFile(player, out string saveFile)) return false;
            using var s = File.OpenRead(saveFile);
            using var r = new BinaryReader(s);
            for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
            {
                field[x, y] = new Cell
                {
                    Value = r.ReadByte(),
                    Locked = r.ReadInt32()
                };
            }
            return true;
        }

        public bool HasSaveFile(Player player, out string saveFile)
        {
            return File.Exists(saveFile = $".\\{Convert.ToBase64String(Encoding.UTF8.GetBytes(player.Name))}.sud");
        }
    }
}
