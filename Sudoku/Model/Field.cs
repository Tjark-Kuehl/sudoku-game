namespace Sudoku.Model
{
    /// <summary>
    ///     A field.
    /// </summary>
    public class Field
    {
        /// <summary>
        ///     The cells.
        /// </summary>
        private Cell[,] _cells;
        /// <summary>
        ///     Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        public Field()
        {
            this._cells = new Cell[9, 9];
        }

        /// <summary>
        ///     Indexer to get or set items within this collection using array index syntax.
        /// </summary>
        /// <param name="x"> The x coordinate. </param>
        /// <param name="y"> The y coordinate. </param>
        /// <returns>
        ///     The indexed item.
        /// </returns>
        public Cell this[int x, int y]
        {
            get { return _cells[x, y]; }
            set { _cells[x, y] = value; }
        }

        /// <summary>
        ///     Copies this object.
        /// </summary>
        /// <returns>
        ///     A Field.
        /// </returns>
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
