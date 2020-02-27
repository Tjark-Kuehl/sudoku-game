using Sudoku.Model;
using System.Collections.Generic;

namespace Sudoku.Control
{
    public class Game
    {
        private readonly Player _player;
        private Field _field;
        public Game(Player player)
        {
            _player = player;
        }

        public bool Load(IGameLoader gameLoader)
        {
          return gameLoader.Load(_player, out _field);
        }

        public void Load(SudokuGenerator.GameDifficulty gameDifficulty)
        {
            _field = SudokuGenerator.GenerateField(gameDifficulty);
        }

        public void Save(IGameLoader gameLoader)
        {
             gameLoader.Save(_player, _field);
        }

        public void Set(int x, int y, byte value)
        {
            if (_field[x, y].Locked == 0)
                _field[x, y].Value = value;
        }

        public bool Islocked(int x, int y)
        {
            return _field[x, y].Locked != 0;
        }

        public Cell Get(int x, int y)
        {
            return _field[x, y];
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
                    if (_field[i, yM].Value == value)
                    {
                        collisions.Add((i, yM));
                    }
                    if (_field[xM, i].Value == value)
                    {
                        collisions.Add((xM, i));
                    }

                    if (_field[x + qX * 3, y + qY * 3].Value == value)
                    {
                        collisions.Add((x + qX * 3, y + qY * 3));
                    }
                }
            }
            if (collisions.Count > 0)
            {
                return false;
            }

            _field[xM, yM].Value = value;
            return true;
        }

        public bool IsFinished()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (_field[x, y].Value == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
