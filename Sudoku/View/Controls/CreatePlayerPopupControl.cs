using System;
using System.Windows.Forms;

namespace Sudoku.View.Controls
{
    public partial class CreatePlayerPopupControl : Form
    {
        public string NewPlayerName => label1.Text;

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

        private void acceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
