using System;
using Sudoku.Model;
using System.Collections.Generic;
using Timer = System.Timers.Timer;

namespace Sudoku.Control
{
    /// <summary>
    ///     A game.
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     Occurs when On Time Changed.
        /// </summary>
        public event EventHandler<int> OnTimeChanged;
        /// <summary>
        ///     Occurs when On Score Changed.
        /// </summary>
        public event EventHandler<int> OnScoreChanged;

        /// <summary>
        ///     The score time.
        /// </summary>
        private DateTime _scoreTime;

        /// <summary>
        ///     The score.
        /// </summary>
        private int _score;

        /// <summary>
        ///     The time.
        /// </summary>
        private int _time;

        /// <summary>
        ///     Gets the player.
        /// </summary>
        /// <value>
        ///     The player.
        /// </value>
        public Player Player { get; }

        /// <summary>
        ///     Gets the field.
        /// </summary>
        /// <value>
        ///     The field.
        /// </value>
        public Field Field { get; private set; }

        /// <summary>
        ///     The timer.
        /// </summary>
        private Timer _timer;

        /// <summary>
        ///     Gets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public int Time
        {
            get { return _time; }
            private set
            {
                _time = value;

                OnTimeChanged?.Invoke(this, _time);
            }
        }

        /// <summary>
        ///     Gets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score
        {
            get { return _score; }
            private set
            {
                _score = value;
                OnScoreChanged?.Invoke(this, _score);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="player"> The player. </param>
        /// <param name="time">   The time. </param>
        /// <param name="score">  The score. </param>
        public Game(Player player, int time, int score)
        {
            Player = player;
            Score = score;
            Time = time;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="player"> The player. </param>
        public Game(Player player)
        {
            Player = player;
        }

        /// <summary>
        ///     Loads this object.
        /// </summary>
        /// <param name="gameDifficulty"> The game difficulty to load. </param>
        public void Load(SudokuGenerator.GameDifficulty gameDifficulty)
        {
            Field = SudokuGenerator.GenerateField(gameDifficulty);
        }
        /// <summary>
        ///     Loads this object.
        /// </summary>
        public void Load()
        {
            Field = new Field();
        }
        /// <summary>
        ///     Starts this object.
        /// </summary>
        public void Start()
        {
            _timer?.Dispose();
            _timer = new Timer(1000);
            _timer.Elapsed += (sender, args) => { Time += 1; };
            _timer.Start();
            _scoreTime = DateTime.Now;
        }

        /// <summary>
        ///     Ends this object.
        /// </summary>
        public void End()
        {
            _timer?.Dispose();
            _timer = null;
        }

        /// <summary>
        ///     Sets.
        /// </summary>
        /// <param name="x">     The x coordinate. </param>
        /// <param name="y">     The y coordinate. </param>
        /// <param name="value"> The value. </param>
        public void Set(int x, int y, byte value)
        {
            if (Field[x, y].Locked == 0)
                Field[x, y].Value = value;
        }

        /// <summary>
        ///     Sets.
        /// </summary>
        /// <param name="x"> The x coordinate. </param>
        /// <param name="y"> The y coordinate. </param>
        /// <param name="c"> The Cell to process. </param>
        public void Set(int x, int y, Cell c)
        {
            Field[x, y] = c;
        }

        /// <summary>
        ///     Query if Cell at x and y is locked.
        /// </summary>
        /// <param name="x"> The x coordinate. </param>
        /// <param name="y"> The y coordinate. </param>
        /// <returns>
        ///     True if locked, false if not.
        /// </returns>
        public bool IsLocked(int x, int y)
        {
            return Field[x, y].Locked != 0;
        }

        /// <summary>
        ///     Gets.
        /// </summary>
        /// <param name="x"> The x coordinate. </param>
        /// <param name="y"> The y coordinate. </param>
        /// <returns>
        ///     A Cell.
        /// </returns>
        public Cell Get(int x, int y)
        {
            return Field[x, y];
        }

        /// <summary>
        ///     Calculates the score.
        /// </summary>
        /// <param name="x">         The x coordinate. </param>
        /// <param name="y">         The y coordinate. </param>
        /// <param name="collision"> True to collision. </param>
        /// <returns>
        ///     The calculated score.
        /// </returns>
        public int CalculateScore(int x, int y, bool collision)
        {
            var cell = Field[x, y];
            if (collision && (cell.ScoreState & 2) != 2)
            {
                cell.ScoreState |= 2;
                return -50;
            }
            if (!collision && (cell.ScoreState & 1) != 1)
            {
                cell.ScoreState |= 1;
                return 100;
            }
            if (!collision && (cell.ScoreState & 4) != 4)
            {
                cell.ScoreState |= 4;
                return -75;
            }

            return 0;
        }

        /// <summary>
        ///     Attempts to set.
        /// </summary>
        /// <param name="xM">         The x coordinate m. </param>
        /// <param name="yM">         The y coordinate m. </param>
        /// <param name="value">      The value. </param>
        /// <param name="collisions"> [in,out] The collisions. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        public bool TrySet(int xM, int yM, byte value, ref IList<(int, int)> collisions)
        {
            int qX = xM / 3;
            int qY = yM / 3;
            collisions.Clear();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int i = x + y * 3;
                    if (Field[i, yM].Value == value)
                    {
                        collisions.Add((i, yM));
                    }
                    if (Field[xM, i].Value == value)
                    {
                        collisions.Add((xM, i));
                    }

                    if (Field[x + qX * 3, y + qY * 3].Value == value)
                    {
                        collisions.Add((x + qX * 3, y + qY * 3));
                    }
                }
            }

            var scoreTime = DateTime.Now;
            var tb = scoreTime - _scoreTime;
            var factor = ScoreFactor(tb.Milliseconds);
            _scoreTime = DateTime.Now;

            if (collisions.Count > 0)
            {
                this.Score += (int)(CalculateScore(xM, yM, true) * factor);
                return false;
            }

            Field[xM, yM].Value = value;
            this.Score += (int)(CalculateScore(xM, yM, false) * factor);
            return true;
        }

        /// <summary>
        ///     Score factor.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <returns>
        ///     A float.
        /// </returns>
        private static float ScoreFactor(int value)
        {
            return 1f + (4f / ((value / 2000f) + 1f));
        }

        /// <summary>
        ///     Query if this object is finished.
        /// </summary>
        /// <returns>
        ///     True if finished, false if not.
        /// </returns>
        public bool IsFinished()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (Field[x, y].Value == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
