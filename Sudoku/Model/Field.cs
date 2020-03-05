namespace Sudoku.Model
{
    public class Field
    {
        private Cell[,] _cells;
        public Field()
        {
            this._cells = new Cell[9, 9];
        }

        public Cell this[int x, int y]
        {
            get { return _cells[x, y]; }
            set { _cells[x, y] = value; }
        }

        public Field Copy()
        {
            Field f = new Field();
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    f[x, y] = new Cell
                    {
                        Value = _cells[x, y].Value,
                        Locked = _cells[x, y].Locked
                    };
                }
            return f;
        }
    }
}
