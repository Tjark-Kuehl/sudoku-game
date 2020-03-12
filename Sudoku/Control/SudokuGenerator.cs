using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Control
{

    /// <summary>
    ///     A sudoku generator.
    /// </summary>
    public static class SudokuGenerator
    {
        /// <summary>
        ///     The random.
        /// </summary>
        private static Random s_rnd = new Random();

        /// <summary>
        ///     A game difficulty.
        /// </summary>
        public abstract class GameDifficulty
        { 
            /// <summary>
            ///     Initializes a new instance of the <see cref="GameDifficulty"/> class.
            /// </summary>
            protected GameDifficulty()
            {
                
            }

           /// <summary>
           ///  Gets the number of empty fields.
           /// </summary>
           /// <value>
           ///  The number of empty fields.
           /// </value>
           public abstract int EmptyFieldCount { get; }

           /// <summary>
           ///  An easy. This class cannot be inherited.
           /// </summary>
           public sealed class Easy : GameDifficulty
           {
               /// <summary>
               ///  Gets the number of empty fields.
               /// </summary>
               /// <value>
               ///  The number of empty fields.
               /// </value>
               public override int EmptyFieldCount
               {
                   get { return 40; }
               }

               /// <summary>
               ///  The default.
               /// </summary>
               public static readonly Easy Default = new Easy();
           }

           /// <summary>
           ///  A medium. This class cannot be inherited.
           /// </summary>
           public sealed class Medium : GameDifficulty
           {
               /// <summary>
               ///  Gets the number of empty fields.
               /// </summary>
               /// <value>
               ///  The number of empty fields.
               /// </value>
               public override int EmptyFieldCount
               {
                   get { return 50; }
               }

               /// <summary>
               ///  The default.
               /// </summary>
               public static readonly Medium Default = new Medium();
            }

           /// <summary>
           ///  A hard. This class cannot be inherited.
           /// </summary>
           public sealed class Hard : GameDifficulty
           {
               /// <summary>
               ///  Gets the number of empty fields.
               /// </summary>
               /// <value>
               ///  The number of empty fields.
               /// </value>
               public override int EmptyFieldCount
               {
                   get { return 60; }
               }

               /// <summary>
               ///  The default.
               /// </summary>
               public static readonly Hard Default = new Hard();
            }
        }


        /// <summary>
        ///     Generates a field.
        /// </summary>
        /// <param name="gameDifficulty">      The game difficulty. </param>
        /// <param name="possibleSolutionMax"> (Optional) The possible solution maximum. </param>
        /// <returns>
        ///     The field.
        /// </returns>
        public static Field GenerateField(GameDifficulty gameDifficulty, int possibleSolutionMax = 80)
        {
        retry:
            Field field = new Field();
            // Generate first 'row' with random possibilities
            Stack<byte> xs = new Stack<byte>(Possibilities.OrderBy(s => s_rnd.Next()));
            for (int x = 0; x < 9; x++)
            {
                field[x, 0] = new Cell { Value = xs.Pop() };
            }

            // Skip first line and continue filling up
            for (int y = 1; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    List<byte> p = new List<byte>();
                    for (int xl = 0; xl < x; xl++)
                    {
                        p.Add(field[xl, y].Value);
                    }
                    for (int yl = 0; yl < y; yl++)
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

                    var possibleValues = Possibilities.Where(s => !p.Contains(s)).OrderBy(s => s_rnd.Next());
                    // ReSharper disable once PossibleMultipleEnumeration
                    if (!possibleValues.Any())
                    {
                        goto retry;
                    }
                    field[x, y] = new Cell
                    {
                        // ReSharper disable once PossibleMultipleEnumeration
                        Value = possibleValues.First()
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

        /// <summary>
        ///     Enumerates solutions in this collection.
        /// </summary>
        /// <param name="field"> The field. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process solutions in this collection.
        /// </returns>
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

        /// <summary>
        ///     Calculates all possibilities for cell.
        /// </summary>
        /// <param name="field"> The field. </param>
        /// <param name="cx">    The cx. </param>
        /// <param name="cy">    The cy. </param>
        /// <returns>
        ///     The calculated all possibilities for cell.
        /// </returns>
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
                    if (qx + xq == cx && qy + yq == cy || field[qx + xq, qy + yq].Locked == 0) continue;
                    p.Add(field[qx + xq, qy + yq].Value);
                }
            return Possibilities.Except(p.Distinct()).ToList();
        }

        /// <summary>
        ///     The possibilities.
        /// </summary>
        private static readonly byte[] Possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary>
        ///     Gets free cell.
        /// </summary>
        /// <param name="field">         The field. </param>
        /// <param name="fx">            [out] The effects. </param>
        /// <param name="fy">            [out] The fy. </param>
        /// <param name="possibilities"> [out] The possibilities. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
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

        /// <summary>
        ///     Enumerates generate solution in this collection.
        /// </summary>
        /// <param name="f"> The Field to process. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process generate solution in this collection.
        /// </returns>
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
