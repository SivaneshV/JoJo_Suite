namespace JoJoSuite.Logic
{
    partial class LogicWhile
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
            this.pnlFirst = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlWhile = new System.Windows.Forms.Panel();
            this.pnlWhileMain = new System.Windows.Forms.Panel();
            this.lblWhile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlFirst.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlWhile.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFirst
            // 
            this.pnlFirst.AutoSize = true;
            this.pnlFirst.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlFirst.Controls.Add(this.pnlMain);
            this.pnlFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFirst.Location = new System.Drawing.Point(0, 0);
            this.pnlFirst.Name = "pnlFirst";
            this.pnlFirst.Padding = new System.Windows.Forms.Padding(1);
            this.pnlFirst.Size = new System.Drawing.Size(250, 119);
            this.pnlFirst.TabIndex = 5;
            // 
            // pnlMain
            // 
            this.pnlMain.AutoSize = true;
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlWhile);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblCondition);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(1, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(248, 117);
            this.pnlMain.TabIndex = 3;
            this.pnlMain.Click += new System.EventHandler(this.pnlMain_Click);
            // 
            // pnlWhile
            // 
            this.pnlWhile.AutoSize = true;
            this.pnlWhile.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlWhile.Controls.Add(this.pnlWhileMain);
            this.pnlWhile.Controls.Add(this.lblWhile);
            this.pnlWhile.Location = new System.Drawing.Point(5, 69);
            this.pnlWhile.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlWhile.Name = "pnlWhile";
            this.pnlWhile.Padding = new System.Windows.Forms.Padding(1);
            this.pnlWhile.Size = new System.Drawing.Size(238, 42);
            this.pnlWhile.TabIndex = 15;
            // 
            // pnlWhileMain
            // 
            this.pnlWhileMain.AllowDrop = true;
            this.pnlWhileMain.AutoSize = true;
            this.pnlWhileMain.BackColor = System.Drawing.Color.White;
            this.pnlWhileMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWhileMain.Location = new System.Drawing.Point(1, 25);
            this.pnlWhileMain.Name = "pnlWhileMain";
            this.pnlWhileMain.Size = new System.Drawing.Size(236, 16);
            this.pnlWhileMain.TabIndex = 8;
            this.pnlWhileMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWhileMain_DragDrop);
            this.pnlWhileMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWhileMain_DragEnter);
            // 
            // lblWhile
            // 
            this.lblWhile.BackColor = System.Drawing.SystemColors.Control;
            this.lblWhile.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWhile.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhile.Location = new System.Drawing.Point(1, 1);
            this.lblWhile.Name = "lblWhile";
            this.lblWhile.Size = new System.Drawing.Size(236, 24);
            this.lblWhile.TabIndex = 7;
            this.lblWhile.Text = "While";
            this.lblWhile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWhile.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Condition";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.ForeColor = System.Drawing.Color.Blue;
            this.lblCondition.Location = new System.Drawing.Point(14, 45);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(51, 13);
            this.lblCondition.TabIndex = 4;
            this.lblCondition.Text = "Condition";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.Control;
            this.pnlHeader.Controls.Add(this.txtTitle);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(5);
            this.pnlHeader.Size = new System.Drawing.Size(248, 26);
            this.pnlHeader.TabIndex = 3;
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Control;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTitle.Location = new System.Drawing.Point(21, 5);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(222, 16);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.Text = "While";
            this.txtTitle.Click += new System.EventHandler(this.pnlMain_Click);
            this.txtTitle.Enter += new System.EventHandler(this.txtTitle_Enter);
            this.txtTitle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTitle_KeyUp);
            this.txtTitle.Leave += new System.EventHandler(this.txtTitle_Leave);
            this.txtTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            this.txtTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            this.txtTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::JoJoSuite.Logic.Properties.Resources.logic02_16;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LogicWhile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlFirst);
            this.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LogicWhile";
            this.Size = new System.Drawing.Size(250, 119);
            this.pnlFirst.ResumeLayout(false);
            this.pnlFirst.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlWhile.ResumeLayout(false);
            this.pnlWhile.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlFirst;
        private System.Windows.Forms.Panel pnlWhile;
        private System.Windows.Forms.Label lblWhile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Panel pnlWhileMain;
        public System.Windows.Forms.Panel pnlMain;
    }
}
