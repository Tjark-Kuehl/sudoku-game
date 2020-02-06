using System.IO;

namespace Sudoku
{
    class SaveGameHelper
    {
        void SaveGame(Cell[,] field, Cell[,] solution)
        {
            using (Stream s = File.OpenWrite(".\\savegame.sud"))
            using (BinaryWriter w = new BinaryWriter(s))
            {
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        w.Write(field[x, y].Value);
                        w.Write(field[x, y].Locked);
                    }

                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        w.Write(solution[x, y].Value);
                        w.Write(solution[x, y].Locked);
                    }
            }
        }

        void LoadGame(out Cell[,] field, out Cell[,] solution)
        {
            field = new Cell[9, 9];
            solution = new Cell[9, 9];
            using (Stream s = File.OpenWrite(".\\savegame.sud"))
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

                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        solution[x, y] = new Cell
                        {
                            Value = r.ReadByte(),
                            Locked = r.ReadInt32()
                        };
                    }
            }
        }
    }
}
