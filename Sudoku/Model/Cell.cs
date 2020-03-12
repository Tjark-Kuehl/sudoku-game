namespace Sudoku.Model
{
    /// <summary>
    ///     A cell.
    /// </summary>
    public class Cell
    {
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public byte Value { get; set; }

        /// <summary>
        ///     Gets or sets the locked.
        /// </summary>
        /// <value>
        ///     The locked.
        /// </value>
        public int Locked { get; set; } = 1;

        /// <summary>
        ///     Gets or sets the state of the score.
        /// </summary>
        /// <value>
        ///     The score state.
        /// </value>
        public byte ScoreState { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Value} = {Locked}";
        }
    }
}
