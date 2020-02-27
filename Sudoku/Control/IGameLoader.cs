using Sudoku.Model;

namespace Sudoku
{
    public interface IGameLoader
    {
        void Save(Player player, Field field);
        bool Load(Player player, out Field field);
        bool HasSaveFile(Player player, out string saveFile);
    }
}