using System.Windows.Forms;

namespace Sudoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.field1 = new Sudoku.Controls.Field();
            this.SuspendLayout();
            // 
            // field1
            // 
            this.field1.BackColor = System.Drawing.SystemColors.Control;
            this.field1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.field1.Location = new System.Drawing.Point(0, 0);
            this.field1.Name = "field1";
            this.field1.Padding = new System.Windows.Forms.Padding(10);
            this.field1.Size = new System.Drawing.Size(784, 761);
            this.field1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.field1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Sudoku [Version 1.0.0.0] [by Tjark Kühl]";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.FieldBase field1;
    }
}

