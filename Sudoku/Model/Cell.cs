namespace Sudoku.Model
{
    public class Cell
    {
        public byte Value { get; set; }
        public int Locked { get; set; } = 1;

        public override string ToString()
        {
            return $"{Value} = {Locked}";
        }
    }
}
