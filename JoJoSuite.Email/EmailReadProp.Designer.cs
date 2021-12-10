namespace JoJoSuite.Email
{
    partial class EmailReadProp
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
            this.piSession = new JoJoSuite.Base.PropInput();
            this.piEmail = new JoJoSuite.Base.PropInput();
            this.piFolder = new JoJoSuite.Base.PropInput();
            this.piSubject = new JoJoSuite.Base.PropInput();
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
            this.pnlHeader.Size = new System.Drawing.Size(280, 25);
            this.pnlHeader.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(21, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(254, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Email Read";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::JoJoSuite.Email.Properties.Resources.email01;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 15);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // piSession
            // 
            this.piSession.BackColor = System.Drawing.Color.White;
            this.piSession.Dock = System.Windows.Forms.DockStyle.Top;
            this.piSession.Location = new System.Drawing.Point(0, 25);
            this.piSession.Name = "piSession";
            this.piSession.Size = new System.Drawing.Size(280, 25);
            this.piSession.TabIndex = 35;
            this.piSession.Title = "Session";
            this.piSession.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piSession.Value = "";
            this.piSession.PropertyChanged += new System.EventHandler(this.piSession_PropertyChanged);
            // 
            // piEmail
            // 
            this.piEmail.BackColor = System.Drawing.Color.White;
            this.piEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.piEmail.Location = new System.Drawing.Point(0, 50);
            this.piEmail.Name = "piEmail";
            this.piEmail.Size = new System.Drawing.Size(280, 25);
            this.piEmail.TabIndex = 36;
            this.piEmail.Title = "Email";
            this.piEmail.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piEmail.Value = "";
            this.piEmail.PropertyChanged += new System.EventHandler(this.piEmail_PropertyChanged);
            // 
            // piFolder
            // 
            this.piFolder.BackColor = System.Drawing.Color.White;
            this.piFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.piFolder.Location = new System.Drawing.Point(0, 75);
            this.piFolder.Name = "piFolder";
            this.piFolder.Size = new System.Drawing.Size(280, 25);
            this.piFolder.TabIndex = 37;
            this.piFolder.Title = "Folder";
            this.piFolder.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piFolder.Value = "";
            this.piFolder.PropertyChanged += new System.EventHandler(this.piFolder_PropertyChanged);
            // 
            // piSubject
            // 
            this.piSubject.BackColor = System.Drawing.Color.White;
            this.piSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.piSubject.Location = new System.Drawing.Point(0, 100);
            this.piSubject.Name = "piSubject";
            this.piSubject.Size = new System.Drawing.Size(280, 25);
            this.piSubject.TabIndex = 38;
            this.piSubject.Title = "Subject";
            this.piSubject.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piSubject.Value = "";
            this.piSubject.PropertyChanged += new System.EventHandler(this.piSubject_PropertyChanged);
            // 
            // EmailReadProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.piSubject);
            this.Controls.Add(this.piFolder);
            this.Controls.Add(this.piEmail);
            this.Controls.Add(this.piSession);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EmailReadProp";
            this.Size = new System.Drawing.Size(280, 151);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Base.PropInput piSession;
        private Base.PropInput piEmail;
        private Base.PropInput piFolder;
        private Base.PropInput piSubject;
    }
}
