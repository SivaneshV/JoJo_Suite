﻿namespace JoJoSuite.Pdf
{
    partial class PdfToHtml
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.pnlFirst = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlFirst.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblUser);
            this.pnlMain.Controls.Add(this.lblUrl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(1, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(198, 128);
            this.pnlMain.TabIndex = 3;
            this.pnlMain.Click += new System.EventHandler(this.pnlMain_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Output";
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
            this.pnlHeader.Size = new System.Drawing.Size(198, 26);
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
            this.txtTitle.Size = new System.Drawing.Size(172, 16);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.Text = "Title";
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
            this.pictureBox1.Image = global::JoJoSuite.Pdf.Properties.Resources.pdf01;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Input";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.ForeColor = System.Drawing.Color.Blue;
            this.lblUser.Location = new System.Drawing.Point(15, 72);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(73, 13);
            this.lblUser.TabIndex = 14;
            this.lblUser.Text = "Code Module:";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.ForeColor = System.Drawing.Color.Blue;
            this.lblUrl.Location = new System.Drawing.Point(15, 53);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(32, 13);
            this.lblUrl.TabIndex = 13;
            this.lblUrl.Text = "Path:";
            // 
            // pnlFirst
            // 
            this.pnlFirst.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlFirst.Controls.Add(this.pnlMain);
            this.pnlFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFirst.Location = new System.Drawing.Point(0, 0);
            this.pnlFirst.Name = "pnlFirst";
            this.pnlFirst.Padding = new System.Windows.Forms.Padding(1);
            this.pnlFirst.Size = new System.Drawing.Size(200, 130);
            this.pnlFirst.TabIndex = 5;
            // 
            // PdfToHtml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.pnlFirst);
            this.Name = "PdfToHtml";
            this.Size = new System.Drawing.Size(200, 130);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlFirst.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFirst;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblUrl;
    }
}
