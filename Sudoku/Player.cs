using System;

namespace Sudoku
{
    class Player
    {
        public string Name { get; }
        public int GameCount { get; }
        public int Score { get; }
        public int Playtime { get; }
        public DateTime Created { get; }

        public Player(string name)
        {
            Name = name;
        }
    }
}
