namespace JoJoSuite.Logic
{
    partial class LogicAssignProp
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.piValue = new JoJoSuite.Base.PropInput();
            this.piTo = new JoJoSuite.Base.PropInput();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.Control;
            this.pnlHeader.Controls.Add(this.label5);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(5);
            this.pnlHeader.Size = new System.Drawing.Size(302, 25);
            this.pnlHeader.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(21, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Assign Value";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::JoJoSuite.Logic.Properties.Resources.logic02_16;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 15);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // piValue
            // 
            this.piValue.BackColor = System.Drawing.Color.White;
            this.piValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.piValue.Location = new System.Drawing.Point(0, 50);
            this.piValue.Name = "piValue";
            this.piValue.Size = new System.Drawing.Size(302, 25);
            this.piValue.TabIndex = 37;
            this.piValue.Title = "Value";
            this.piValue.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piValue.Value = "";
            this.piValue.PropertyChanged += new System.EventHandler(this.piValue_PropertyChanged);
            // 
            // piTo
            // 
            this.piTo.BackColor = System.Drawing.Color.White;
            this.piTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.piTo.Location = new System.Drawing.Point(0, 25);
            this.piTo.Name = "piTo";
            this.piTo.Size = new System.Drawing.Size(302, 25);
            this.piTo.TabIndex = 36;
            this.piTo.Title = "To";
            this.piTo.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piTo.Value = "";
            this.piTo.PropertyChanged += new System.EventHandler(this.piTo_PropertyChanged);
            // 
            // LogicAssignProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.piValue);
            this.Controls.Add(this.piTo);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LogicAssignProp";
            this.Size = new System.Drawing.Size(302, 102);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Base.PropInput piTo;
        private Base.PropInput piValue;
    }
}
