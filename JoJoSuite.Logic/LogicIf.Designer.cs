namespace JoJoSuite.Logic
{
    partial class LogicIf
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.tlpTrFl = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFalse = new System.Windows.Forms.Panel();
            this.pnlFalseMain = new System.Windows.Forms.Panel();
            this.lblFalse = new System.Windows.Forms.Label();
            this.pnlTrue = new System.Windows.Forms.Panel();
            this.pnlTrueMain = new System.Windows.Forms.Panel();
            this.lblTrue = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlFirst.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tlpTrFl.SuspendLayout();
            this.pnlFalse.SuspendLayout();
            this.pnlTrue.SuspendLayout();
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
            this.pnlFirst.Size = new System.Drawing.Size(460, 172);
            this.pnlFirst.TabIndex = 6;
            // 
            // pnlMain
            // 
            this.pnlMain.AutoSize = true;
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblCondition);
            this.pnlMain.Controls.Add(this.tlpTrFl);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(1, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(458, 170);
            this.pnlMain.TabIndex = 3;
            this.pnlMain.Click += new System.EventHandler(this.pnlMain_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Condition";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.ForeColor = System.Drawing.Color.Blue;
            this.lblCondition.Location = new System.Drawing.Point(8, 50);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(51, 13);
            this.lblCondition.TabIndex = 16;
            this.lblCondition.Text = "Condition";
            // 
            // tlpTrFl
            // 
            this.tlpTrFl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpTrFl.AutoSize = true;
            this.tlpTrFl.ColumnCount = 2;
            this.tlpTrFl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tlpTrFl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 238F));
            this.tlpTrFl.Controls.Add(this.pnlFalse, 0, 0);
            this.tlpTrFl.Controls.Add(this.pnlTrue, 0, 0);
            this.tlpTrFl.Location = new System.Drawing.Point(5, 72);
            this.tlpTrFl.Name = "tlpTrFl";
            this.tlpTrFl.RowCount = 1;
            this.tlpTrFl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrFl.Size = new System.Drawing.Size(448, 95);
            this.tlpTrFl.TabIndex = 15;
            // 
            // pnlFalse
            // 
            this.pnlFalse.AutoSize = true;
            this.pnlFalse.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlFalse.Controls.Add(this.pnlFalseMain);
            this.pnlFalse.Controls.Add(this.lblFalse);
            this.pnlFalse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFalse.Location = new System.Drawing.Point(213, 0);
            this.pnlFalse.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pnlFalse.Name = "pnlFalse";
            this.pnlFalse.Padding = new System.Windows.Forms.Padding(1);
            this.pnlFalse.Size = new System.Drawing.Size(235, 95);
            this.pnlFalse.TabIndex = 16;
            // 
            // pnlFalseMain
            // 
            this.pnlFalseMain.AllowDrop = true;
            this.pnlFalseMain.AutoSize = true;
            this.pnlFalseMain.BackColor = System.Drawing.Color.White;
            this.pnlFalseMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFalseMain.Location = new System.Drawing.Point(1, 25);
            this.pnlFalseMain.Name = "pnlFalseMain";
            this.pnlFalseMain.Size = new System.Drawing.Size(233, 69);
            this.pnlFalseMain.TabIndex = 9;
            this.pnlFalseMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlFalseMain_DragDrop);
            this.pnlFalseMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlFalseMain_DragEnter);
            // 
            // lblFalse
            // 
            this.lblFalse.BackColor = System.Drawing.SystemColors.Control;
            this.lblFalse.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFalse.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFalse.Location = new System.Drawing.Point(1, 1);
            this.lblFalse.Name = "lblFalse";
            this.lblFalse.Size = new System.Drawing.Size(233, 24);
            this.lblFalse.TabIndex = 7;
            this.lblFalse.Text = "Else";
            this.lblFalse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTrue
            // 
            this.pnlTrue.AutoSize = true;
            this.pnlTrue.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlTrue.Controls.Add(this.pnlTrueMain);
            this.pnlTrue.Controls.Add(this.lblTrue);
            this.pnlTrue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTrue.Location = new System.Drawing.Point(0, 0);
            this.pnlTrue.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlTrue.Name = "pnlTrue";
            this.pnlTrue.Padding = new System.Windows.Forms.Padding(1);
            this.pnlTrue.Size = new System.Drawing.Size(207, 95);
            this.pnlTrue.TabIndex = 15;
            // 
            // pnlTrueMain
            // 
            this.pnlTrueMain.AllowDrop = true;
            this.pnlTrueMain.AutoSize = true;
            this.pnlTrueMain.BackColor = System.Drawing.Color.White;
            this.pnlTrueMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTrueMain.Location = new System.Drawing.Point(1, 25);
            this.pnlTrueMain.Name = "pnlTrueMain";
            this.pnlTrueMain.Size = new System.Drawing.Size(205, 69);
            this.pnlTrueMain.TabIndex = 8;
            this.pnlTrueMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlTrueMain_DragDrop);
            this.pnlTrueMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlTrueMain_DragEnter);
            // 
            // lblTrue
            // 
            this.lblTrue.BackColor = System.Drawing.SystemColors.Control;
            this.lblTrue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTrue.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrue.Location = new System.Drawing.Point(1, 1);
            this.lblTrue.Name = "lblTrue";
            this.lblTrue.Size = new System.Drawing.Size(205, 24);
            this.lblTrue.TabIndex = 7;
            this.lblTrue.Text = "Then";
            this.lblTrue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.pnlHeader.Size = new System.Drawing.Size(458, 26);
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
            this.txtTitle.Size = new System.Drawing.Size(432, 16);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.Text = "If";
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
            // LogicIf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlFirst);
            this.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LogicIf";
            this.Size = new System.Drawing.Size(460, 172);
            this.pnlFirst.ResumeLayout(false);
            this.pnlFirst.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tlpTrFl.ResumeLayout(false);
            this.tlpTrFl.PerformLayout();
            this.pnlFalse.ResumeLayout(false);
            this.pnlFalse.PerformLayout();
            this.pnlTrue.ResumeLayout(false);
            this.pnlTrue.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlFirst;
        public System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCondition;
        public System.Windows.Forms.TableLayoutPanel tlpTrFl;
        private System.Windows.Forms.Panel pnlFalse;
        public System.Windows.Forms.Panel pnlFalseMain;
        private System.Windows.Forms.Label lblFalse;
        private System.Windows.Forms.Panel pnlTrue;
        public System.Windows.Forms.Panel pnlTrueMain;
        private System.Windows.Forms.Label lblTrue;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
