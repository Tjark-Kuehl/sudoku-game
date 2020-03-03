using System.Collections.Generic;
using Sudoku.Model;

namespace Sudoku.Control
{
    public interface IPlayerLoader
    {
        void SavePlayer(Player player);
        IEnumerable<Player> LoadPlayers();
    }
}