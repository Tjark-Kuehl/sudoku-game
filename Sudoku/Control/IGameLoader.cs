using System.IO;
using Sudoku.Model;

namespace Sudoku.Control
{
    public interface IGameLoader
    {
        void Save(Game game);
        bool Load(Player player, out Game game);
        bool HasSaveFile(Player player, out FileInfo saveFile);
    }
}