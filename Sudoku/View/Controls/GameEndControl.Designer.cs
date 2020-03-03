namespace Sudoku.View.Controls
{
    partial class GameEndControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.score_label = new System.Windows.Forms.Label();
            this.time_label = new System.Windows.Forms.Label();
            this.toMainMenu_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(544, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Herzlichen Glückwunsch du kleiner Wicht, du hast das Sudoku gelöst\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // score_label
            // 
            this.score_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.score_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_label.Location = new System.Drawing.Point(3, 0);
            this.score_label.Name = "score_label";
            this.score_label.Size = new System.Drawing.Size(538, 50);
            this.score_label.TabIndex = 1;
            this.score_label.Text = "Score: ";
            this.score_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // time_label
            // 
            this.time_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.time_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_label.Location = new System.Drawing.Point(3, 50);
            this.time_label.Name = "time_label";
            this.time_label.Size = new System.Drawing.Size(538, 50);
            this.time_label.TabIndex = 2;
            this.time_label.Text = "Zeit: ";
            this.time_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toMainMenu_button
            // 
            this.toMainMenu_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toMainMenu_button.Location = new System.Drawing.Point(100, 5);
            this.toMainMenu_button.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.toMainMenu_button.Name = "toMainMenu_button";
            this.toMainMenu_button.Size = new System.Drawing.Size(344, 29);
            this.toMainMenu_button.TabIndex = 5;
            this.toMainMenu_button.Text = "Zum Hauptmenü";
            this.toMainMenu_button.UseVisualStyleBackColor = true;
            this.toMainMenu_button.Click += new System.EventHandler(this.toMainMenu_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toMainMenu_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 190);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(100, 5, 100, 5);
            this.panel1.Size = new System.Drawing.Size(544, 39);
            this.panel1.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.score_label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.time_label, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 130);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // GameEndControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "GameEndControl";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(564, 239);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label score_label;
        private System.Windows.Forms.Label time_label;
        private System.Windows.Forms.Button toMainMenu_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
