namespace Sudoku.Model
{
    class Field
    {
        private Cell[,] _cells;
        public Field()
        {
            this._cells = new Cell[9, 9];
        }

        public Cell this[int x, int y] 
        { 
            get { return this._cells[x, y]; }
            set { this._cells[x, y] = value; }
        }
    }
}
