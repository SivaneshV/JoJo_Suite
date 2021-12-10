namespace JoJoSuite.Logic
{
    partial class LogicFor
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
            this.pnlFor = new System.Windows.Forms.Panel();
            this.pnlForMain = new System.Windows.Forms.Panel();
            this.lblFor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCollection = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlFirst.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlFor.SuspendLayout();
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
            this.pnlMain.Controls.Add(this.pnlFor);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblCollection);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(1, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(248, 117);
            this.pnlMain.TabIndex = 3;
            this.pnlMain.Click += new System.EventHandler(this.pnlMain_Click);
            // 
            // pnlFor
            // 
            this.pnlFor.AutoSize = true;
            this.pnlFor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlFor.Controls.Add(this.pnlForMain);
            this.pnlFor.Controls.Add(this.lblFor);
            this.pnlFor.Location = new System.Drawing.Point(5, 69);
            this.pnlFor.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlFor.Name = "pnlFor";
            this.pnlFor.Padding = new System.Windows.Forms.Padding(1);
            this.pnlFor.Size = new System.Drawing.Size(238, 42);
            this.pnlFor.TabIndex = 15;
            // 
            // pnlForMain
            // 
            this.pnlForMain.AllowDrop = true;
            this.pnlForMain.AutoSize = true;
            this.pnlForMain.BackColor = System.Drawing.Color.White;
            this.pnlForMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForMain.Location = new System.Drawing.Point(1, 25);
            this.pnlForMain.Name = "pnlForMain";
            this.pnlForMain.Size = new System.Drawing.Size(236, 16);
            this.pnlForMain.TabIndex = 8;
            this.pnlForMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWhileMain_DragDrop);
            this.pnlForMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWhileMain_DragEnter);
            // 
            // lblFor
            // 
            this.lblFor.BackColor = System.Drawing.SystemColors.Control;
            this.lblFor.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFor.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFor.Location = new System.Drawing.Point(1, 1);
            this.lblFor.Name = "lblFor";
            this.lblFor.Size = new System.Drawing.Size(236, 24);
            this.lblFor.TabIndex = 7;
            this.lblFor.Text = "For Each";
            this.lblFor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Collection";
            // 
            // lblCollection
            // 
            this.lblCollection.AutoSize = true;
            this.lblCollection.ForeColor = System.Drawing.Color.Blue;
            this.lblCollection.Location = new System.Drawing.Point(14, 45);
            this.lblCollection.Name = "lblCollection";
            this.lblCollection.Size = new System.Drawing.Size(51, 13);
            this.lblCollection.TabIndex = 4;
            this.lblCollection.Text = "Collection";
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
            this.txtTitle.Text = "For Each";
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
            // LogicFor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlFirst);
            this.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LogicFor";
            this.Size = new System.Drawing.Size(250, 119);
            this.pnlFirst.ResumeLayout(false);
            this.pnlFirst.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlFor.ResumeLayout(false);
            this.pnlFor.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlFirst;
        private System.Windows.Forms.Panel pnlFor;
        private System.Windows.Forms.Label lblFor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCollection;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Panel pnlForMain;
        public System.Windows.Forms.Panel pnlMain;
    }
}
