namespace Sudoku.View.Controls
{
    partial class GameMenuControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.top_pnael = new System.Windows.Forms.Panel();
            this.score = new System.Windows.Forms.Label();
            this.score_label = new System.Windows.Forms.Label();
            this.bottom_panel = new System.Windows.Forms.Panel();
            this.time = new System.Windows.Forms.Label();
            this.time_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.top_pnael.SuspendLayout();
            this.bottom_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.top_pnael, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bottom_panel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(739, 516);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // top_pnael
            // 
            this.top_pnael.Controls.Add(this.score);
            this.top_pnael.Controls.Add(this.score_label);
            this.top_pnael.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top_pnael.Location = new System.Drawing.Point(3, 3);
            this.top_pnael.Name = "top_pnael";
            this.top_pnael.Size = new System.Drawing.Size(733, 19);
            this.top_pnael.TabIndex = 0;
            // 
            // score
            // 
            this.score.Dock = System.Windows.Forms.DockStyle.Left;
            this.score.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score.Location = new System.Drawing.Point(58, 0);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(53, 19);
            this.score.TabIndex = 1;
            this.score.Text = "0";
            this.score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // score_label
            // 
            this.score_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.score_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_label.Location = new System.Drawing.Point(0, 0);
            this.score_label.Name = "score_label";
            this.score_label.Size = new System.Drawing.Size(58, 19);
            this.score_label.TabIndex = 0;
            this.score_label.Text = "Score:";
            this.score_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bottom_panel
            // 
            this.bottom_panel.Controls.Add(this.time);
            this.bottom_panel.Controls.Add(this.time_label);
            this.bottom_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottom_panel.Location = new System.Drawing.Point(3, 494);
            this.bottom_panel.Name = "bottom_panel";
            this.bottom_panel.Size = new System.Drawing.Size(733, 19);
            this.bottom_panel.TabIndex = 1;
            // 
            // time
            // 
            this.time.Dock = System.Windows.Forms.DockStyle.Left;
            this.time.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time.Location = new System.Drawing.Point(58, 0);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(53, 19);
            this.time.TabIndex = 3;
            this.time.Text = "0";
            this.time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // time_label
            // 
            this.time_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.time_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_label.Location = new System.Drawing.Point(0, 0);
            this.time_label.Name = "time_label";
            this.time_label.Size = new System.Drawing.Size(58, 19);
            this.time_label.TabIndex = 2;
            this.time_label.Text = "Time:";
            this.time_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GameMenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GameMenuControl";
            this.Size = new System.Drawing.Size(739, 516);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.top_pnael.ResumeLayout(false);
            this.bottom_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel top_pnael;
        private System.Windows.Forms.Label score_label;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.Panel bottom_panel;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Label time_label;
    }
}
