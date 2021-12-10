namespace JoJoSuite.Email
{
    partial class EmailLoginProp
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
            this.piPwd = new JoJoSuite.Base.PropInput();
            this.piUser = new JoJoSuite.Base.PropInput();
            this.piDomain = new JoJoSuite.Base.PropInput();
            this.piServer = new JoJoSuite.Base.PropInput();
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
            this.pnlHeader.Size = new System.Drawing.Size(327, 25);
            this.pnlHeader.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(21, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(301, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Email Login";
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
            // piPwd
            // 
            this.piPwd.BackColor = System.Drawing.Color.White;
            this.piPwd.Dock = System.Windows.Forms.DockStyle.Top;
            this.piPwd.Location = new System.Drawing.Point(0, 100);
            this.piPwd.Name = "piPwd";
            this.piPwd.Size = new System.Drawing.Size(327, 25);
            this.piPwd.TabIndex = 26;
            this.piPwd.Title = "Password";
            this.piPwd.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piPwd.Value = "";
            this.piPwd.PropertyChanged += new System.EventHandler(this.piPwd_PropertyChanged);
            // 
            // piUser
            // 
            this.piUser.BackColor = System.Drawing.Color.White;
            this.piUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.piUser.Location = new System.Drawing.Point(0, 75);
            this.piUser.Name = "piUser";
            this.piUser.Size = new System.Drawing.Size(327, 25);
            this.piUser.TabIndex = 25;
            this.piUser.Title = "User";
            this.piUser.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piUser.Value = "";
            this.piUser.PropertyChanged += new System.EventHandler(this.piUser_PropertyChanged);
            // 
            // piDomain
            // 
            this.piDomain.BackColor = System.Drawing.Color.White;
            this.piDomain.Dock = System.Windows.Forms.DockStyle.Top;
            this.piDomain.Location = new System.Drawing.Point(0, 50);
            this.piDomain.Name = "piDomain";
            this.piDomain.Size = new System.Drawing.Size(327, 25);
            this.piDomain.TabIndex = 24;
            this.piDomain.Title = "Domain";
            this.piDomain.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piDomain.Value = "";
            this.piDomain.PropertyChanged += new System.EventHandler(this.piDomain_PropertyChanged);
            // 
            // piServer
            // 
            this.piServer.BackColor = System.Drawing.Color.White;
            this.piServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.piServer.Location = new System.Drawing.Point(0, 25);
            this.piServer.Name = "piServer";
            this.piServer.Size = new System.Drawing.Size(327, 25);
            this.piServer.TabIndex = 23;
            this.piServer.Title = "Exchange";
            this.piServer.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piServer.Value = "";
            this.piServer.PropertyChanged += new System.EventHandler(this.piServer_PropertyChanged);
            // 
            // EmailLoginProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.piPwd);
            this.Controls.Add(this.piUser);
            this.Controls.Add(this.piDomain);
            this.Controls.Add(this.piServer);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("HP Simplified", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EmailLoginProp";
            this.Size = new System.Drawing.Size(327, 144);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Base.PropInput piServer;
        private Base.PropInput piDomain;
        private Base.PropInput piUser;
        private Base.PropInput piPwd;
    }
}
