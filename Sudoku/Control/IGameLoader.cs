using Sudoku.Model;

namespace Sudoku
{
    internal interface IGameLoader
    {
        void Save(Player player, Field field);
        Field Load(Player player);
        bool HasSaveFile(Player player, out string saveFile);
    }
}