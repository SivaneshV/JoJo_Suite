namespace JoJoSuite.Database
{
    partial class ConnectToDbProp
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
            this.piUser = new JoJoSuite.Base.PropInput();
            this.piDb = new JoJoSuite.Base.PropInput();
            this.piServer = new JoJoSuite.Base.PropInput();
            this.piPwd = new JoJoSuite.Base.PropInput();
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
            this.pnlHeader.Size = new System.Drawing.Size(310, 25);
            this.pnlHeader.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(21, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(284, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Connect to Database";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::JoJoSuite.Db.Properties.Resources.db01;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 15);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // piUser
            // 
            this.piUser.BackColor = System.Drawing.Color.White;
            this.piUser.Collection = null;
            this.piUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.piUser.Location = new System.Drawing.Point(0, 75);
            this.piUser.Name = "piUser";
            this.piUser.Size = new System.Drawing.Size(310, 25);
            this.piUser.TabIndex = 18;
            this.piUser.Title = "User";
            this.piUser.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piUser.Value = "";
            this.piUser.PropertyChanged += new System.EventHandler(this.piUsr_PropertyChanged);
            // 
            // piDb
            // 
            this.piDb.BackColor = System.Drawing.Color.White;
            this.piDb.Collection = null;
            this.piDb.Dock = System.Windows.Forms.DockStyle.Top;
            this.piDb.Location = new System.Drawing.Point(0, 50);
            this.piDb.Name = "piDb";
            this.piDb.Size = new System.Drawing.Size(310, 25);
            this.piDb.TabIndex = 17;
            this.piDb.Title = "Database";
            this.piDb.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piDb.Value = "";
            this.piDb.PropertyChanged += new System.EventHandler(this.piDb_PropertyChanged);
            // 
            // piServer
            // 
            this.piServer.BackColor = System.Drawing.Color.White;
            this.piServer.Collection = null;
            this.piServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.piServer.Location = new System.Drawing.Point(0, 25);
            this.piServer.Name = "piServer";
            this.piServer.Size = new System.Drawing.Size(310, 25);
            this.piServer.TabIndex = 16;
            this.piServer.Title = "Server";
            this.piServer.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piServer.Value = "";
            this.piServer.PropertyChanged += new System.EventHandler(this.piServer_PropertyChanged);
            // 
            // piPwd
            // 
            this.piPwd.BackColor = System.Drawing.Color.White;
            this.piPwd.Collection = null;
            this.piPwd.Location = new System.Drawing.Point(1, 100);
            this.piPwd.Name = "piPwd";
            this.piPwd.Size = new System.Drawing.Size(310, 25);
            this.piPwd.TabIndex = 19;
            this.piPwd.Title = "Title";
            this.piPwd.Type = JoJoSuite.Base.PropInput.r2rDataType.Password;
            this.piPwd.Value = "";
            // 
            // ConnectToDbProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.piPwd);
            this.Controls.Add(this.piUser);
            this.Controls.Add(this.piDb);
            this.Controls.Add(this.piServer);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ConnectToDbProp";
            this.Size = new System.Drawing.Size(310, 158);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Base.PropInput piServer;
        private Base.PropInput piDb;
        private Base.PropInput piUser;
        private Base.PropInput piPwd;
    }
}
