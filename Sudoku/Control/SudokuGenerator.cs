using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Control
{

    public static class SudokuGenerator
    {
        private static Random s_rnd = new Random();


        public abstract class GameDifficulty
        { 
            protected GameDifficulty()
            {
                
            }

           public abstract int EmptyFieldCount { get; }

           public sealed class Easy : GameDifficulty
           {
               public override int EmptyFieldCount
               {
                   get => 40;
               }

               public static readonly Easy Default = new Easy();
           }

           public sealed class Medium : GameDifficulty
           {
               public override int EmptyFieldCount
               {
                   get => 50;
               }
               public static readonly Medium Default = new Medium();
            }

           public sealed class Hard : GameDifficulty
           {
               public override int EmptyFieldCount
               {
                   get => 60;
               }
               public static readonly Hard Default = new Hard();
            }
        }


        public static Field GenerateField(GameDifficulty gameDifficulty, int possibleSolutionMax = 80)
        {
        retry:
            Field field = new Field();
            Stack<byte> xs = new Stack<byte>(Possibilities.OrderBy(s => s_rnd.Next()));
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

                    var v = Possibilities.Where(s => !p.Contains(s)).OrderBy(s => s_rnd.Next());
                    // ReSharper disable once PossibleMultipleEnumeration
                    if (!v.Any())
                    {
                        goto retry;
                    }
                    field[x, y] = new Cell
                    {
                        // ReSharper disable once PossibleMultipleEnumeration
                        Value = v.First()
                    };
                }
            }

            for (int i = 0; i < gameDifficulty.EmptyFieldCount; i++)
            {
                if (field[s_rnd.Next(9), s_rnd.Next(9)].Value != 0)
                {
                    int rx = s_rnd.Next(9), ry = s_rnd.Next(9);
                    field[rx, ry].Value = 0;
                    field[rx, ry].Locked = 0;
                }
                else i--;
            }

            if(Solutions(field).Count() > possibleSolutionMax)
                goto retry;

            return field;
        }

        public static IEnumerable<Field> Solutions(Field field)
        {
            Field f = new Field();
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    f[x, y] = field[x,y].Locked == 1
                        ? new Cell
                        {
                            Value = field[x, y].Value
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
