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
            Easy = 1,
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

        public static IEnumerable<Field> Solutions(Game game)
        {
            Field f = new Field();
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    f[x, y] = game.Get(x, y).Locked == 1
                        ? new Cell
                        {
                            Value = game.Get(x, y).Value
                        }
                        : new Cell
                        {
                            Locked = 0,
                        };
                }
            return GenerateSolution(f);
        }

        private static IList<byte> CalculateAllPossibilitiesForCell(Field field, int cx, int cy)
        {
            List<byte> p = new List<byte>();

            for (int x = 0; x < 9; x++)
            {
                if (x == cx || field[x, cy].Locked == 0) continue;
                p.Add(field[x, cy].Value);
            }

            for (int y = 0; y < 9; y++)
            {
                if (y == cy || field[cx, y].Locked == 0) continue;
                p.Add(field[cx, y].Value);
            }

            int qx = cx / 3 * 3;
            int qy = cy / 3 * 3;
            for (int yq = 0; yq < 3; yq++)
                for (int xq = 0; xq < 3; xq++)
                {
                    if ((qx + xq == cx && qy + yq == cy) || field[qx + xq, qy + yq].Locked == 0) continue;
                    p.Add(field[qx + xq, qy + yq].Value);
                }
            return Possibilities.Except(p.Distinct()).ToList();
        }

        private static readonly byte[] Possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private static bool GetFreeCell(Field field, out int fx, out int fy, out IList<byte> possibilities)
        {
            for (fy = 0; fy < 9; fy++)
            {
                for (fx = 0; fx < 9; fx++)
                {
                    if (field[fx, fy].Locked == 0)
                    {
                        possibilities = CalculateAllPossibilitiesForCell(field, fx, fy);
                        return true;
                    }
                }
            }

            fy = -1;
            fx = -1;
            possibilities = null;
            return false;
        }

        private static IEnumerable<Field> GenerateSolution(Field f)
        {
            if (GetFreeCell(f, out int fx, out int fy, out IList<byte> possibilities))
            {
                if (possibilities.Count != 0)
                {
                    foreach (byte p in possibilities)
                    {
                        f[fx, fy].Value = p;
                        f[fx, fy].Locked = -1;

                        foreach (var ff in GenerateSolution(f))
                        {
                            yield return ff;
                        }
                    }

                    //backtrack
                    f[fx, fy].Value = 0;
                    f[fx, fy].Locked = 0;
                }
                yield break;
            }
            yield return f;
        }
    }
}
