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
            using (Stream s = File.OpenWrite($".\\{Convert.ToBase64String(Encoding.UTF8.GetBytes(player.Name))}.sud"))
            using (BinaryWriter w = new BinaryWriter(s))
            {
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        w.Write(field[x, y].Value);
                        w.Write(field[x, y].Locked);
                    }
            }
        }

        public Field Load(Player player)
        {
            Field field = new Field();
            if (HasSaveFile(player, out string saveFile))
            {
                using (Stream s = File.OpenRead(saveFile))
                using (BinaryReader r = new BinaryReader(s))
                {
                    for (int x = 0; x < 9; x++)
                        for (int y = 0; y < 9; y++)
                        {
                            field[x, y] = new Cell
                            {
                                Value = r.ReadByte(),
                                Locked = r.ReadInt32()
                            };
                        }
                }
            }
            return field;
        }

        public bool HasSaveFile(Player player, out string saveFile)
        {
            return File.Exists(saveFile = $".\\{Convert.ToBase64String(Encoding.UTF8.GetBytes(player.Name))}.sud");
        }
    }
}
