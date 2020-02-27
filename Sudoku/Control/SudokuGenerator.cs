using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Control
{

    public static class SudokuGenerator
    {
        private static Random s_rnd = new Random();
        public enum GameDifficulty
        {
            Easy = 40,
            Medium = 50,
            Hard = 60
        }
        public static Field GenerateField(GameDifficulty gameDifficulty)
        {
        retry:
            Field field = new Field();
            Stack<byte> xs = new Stack<byte>(Enumerable.Range(1, 9).Select(s => (byte)s).OrderBy(s => s_rnd.Next()));
            for (int x = 0; x < 9; x++)
            {
                field[x, 0] = new Cell { Value = xs.Pop() };
            }

            for (int y = 1; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    List<byte> p = new List<byte>();
                    for (int xl = x - 1; xl >= 0; xl--)
                    {
                        p.Add(field[xl, y].Value);
                    }
                    for (int yl = y - 1; yl >= 0; yl--)
                    {
                        p.Add(field[x, yl].Value);
                    }

                    int qx = x / 3 * 3;
                    int qy = y / 3 * 3;
                    for (int yq = qy; yq < y; yq++)
                    {
                        for (int xq = qx; xq < qx + 3; xq++)
                        {
                            p.Add(field[xq, yq].Value);
                        }
                    }
                    try
                    {
                        field[x, y] = new Cell
                        {
                            Value = Enumerable.Range(1, 9).Select(s => (byte)s).Where(s => !p.Contains(s)).OrderBy(s => s_rnd.Next()).First()
                        };
                    }
                    catch
                    {
                        goto retry;
                    }
                }
            }

            for (int i = 0; i < (int)gameDifficulty; i++)
            {
                if (field[s_rnd.Next(9), s_rnd.Next(9)].Value != 0)
                {
                    int rx = s_rnd.Next(9), ry = s_rnd.Next(9);
                    field[rx, ry].Value = 0;
                    field[rx, ry].Locked = 0;
                }
                else i--;
            }

            return field;
        }
    }
}
