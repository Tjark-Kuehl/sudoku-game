using System;
using System.Windows.Forms;

namespace Sudoku.View.Controls
{
    /// <summary>
    ///     A create player popup control.
    /// </summary>
    public partial class CreatePlayerPopupControl : Form
    {
        /// <summary>
        ///     Gets the name of the new player.
        /// </summary>
        /// <value>
        ///     The name of the new player.
        /// </value>
        public string NewPlayerName => textBox1.Text;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreatePlayerPopupControl"/> class.
        /// </summary>
        public CreatePlayerPopupControl()
        {
            InitializeComponent();
            AcceptButton = acceptButton;
            CancelButton = cancelButton;

            //Hier kämpfte tjark gegen den schlaf,langeweile und einsamkeit an und
            //beschloss das projekt einem fähigen entwickler zu überreichen,
            //der dies zu ende bringen soll...
            //
            //hier beginnt nun also die geschichte eines einsamen entwicklers,
            //der mal wieder die scheiße von anderen ausbaden darf...
        }

        /// <summary>
        ///     Event handler. Called by acceptButton for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void acceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     Event handler. Called by cancelButton for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
