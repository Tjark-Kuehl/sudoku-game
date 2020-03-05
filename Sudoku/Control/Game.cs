using System;
using Sudoku.Model;
using System.Collections.Generic;
using Timer = System.Timers.Timer;

namespace Sudoku.Control
{
    public class Game
    {
        public event EventHandler<int> OnTimeChanged;
        public event EventHandler<int> OnScoreChanged;

        private DateTime _scoreTime;
        private int _score;
        private int _time;
        public Player Player { get; }

        public Field Field { get; private set; }
        private Timer _timer;

        public int Time
        {
            get { return _time; }
            private set
            {
                _time = value;

                OnTimeChanged?.Invoke(this, _time);
            }
        }

        public int Score
        {
            get { return _score; }
            private set
            {
                _score = value;
                OnScoreChanged?.Invoke(this, _score);
            }
        }

        public Game(Player player, int time, int score)
        {
            Player = player;
            Score = score;
            Time = time;
        }

        public Game(Player player)
        {
            Player = player;
        }

        public void Load(SudokuGenerator.GameDifficulty gameDifficulty)
        {
            Field = SudokuGenerator.GenerateField(gameDifficulty);
        }
        public void Load()
        {
            Field = new Field();
        }
        public void Start()
        {
            _timer?.Dispose();
            _timer = new Timer(1000);
            _timer.Elapsed += (sender, args) => { Time += 1; };
            _timer.Start();
            _scoreTime = DateTime.Now;
        }

        public void End()
        {
            _timer?.Dispose();
            _timer = null;
        }

        public void Set(int x, int y, byte value)
        {
            if (Field[x, y].Locked == 0)
                Field[x, y].Value = value;
        }

        public void Set(int x, int y, Cell c)
        {
            Field[x, y] = c;
        }

        public bool IsLocked(int x, int y)
        {
            return Field[x, y].Locked != 0;
        }

        public Cell Get(int x, int y)
        {
            return Field[x, y];
        }

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

        private static float ScoreFactor(int value)
        {
            return 1f + (4f / ((value / 2000f) + 1f));
        }

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
