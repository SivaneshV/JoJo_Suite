namespace JoJoSuite.Web
{
    partial class LoginProp
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
            this.piUrl = new JoJoSuite.Base.PropInput();
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
            this.pnlHeader.Size = new System.Drawing.Size(289, 25);
            this.pnlHeader.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(21, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "HP Login";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::JoJoSuite.Web.Properties.Resources.web01;
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
            this.piPwd.Location = new System.Drawing.Point(0, 75);
            this.piPwd.Name = "piPwd";
            this.piPwd.Size = new System.Drawing.Size(289, 25);
            this.piPwd.TabIndex = 46;
            this.piPwd.Title = "Password";
            this.piPwd.Type = JoJoSuite.Base.PropInput.r2rDataType.Boolean;
            this.piPwd.Value = "";
            this.piPwd.PropertyChanged += new System.EventHandler(this.piPwd_PropertyChanged);
            // 
            // piUser
            // 
            this.piUser.BackColor = System.Drawing.Color.White;
            this.piUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.piUser.Location = new System.Drawing.Point(0, 50);
            this.piUser.Name = "piUser";
            this.piUser.Size = new System.Drawing.Size(289, 25);
            this.piUser.TabIndex = 45;
            this.piUser.Title = "User";
            this.piUser.Type = JoJoSuite.Base.PropInput.r2rDataType.Date;
            this.piUser.Value = "";
            this.piUser.PropertyChanged += new System.EventHandler(this.piUser_PropertyChanged);
            // 
            // piUrl
            // 
            this.piUrl.BackColor = System.Drawing.Color.White;
            this.piUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.piUrl.Location = new System.Drawing.Point(0, 25);
            this.piUrl.Name = "piUrl";
            this.piUrl.Size = new System.Drawing.Size(289, 25);
            this.piUrl.TabIndex = 44;
            this.piUrl.Title = "Url";
            this.piUrl.Type = JoJoSuite.Base.PropInput.r2rDataType.String;
            this.piUrl.Value = "";
            this.piUrl.PropertyChanged += new System.EventHandler(this.piUrl_PropertyChanged);
            // 
            // HPLoginProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.piPwd);
            this.Controls.Add(this.piUser);
            this.Controls.Add(this.piUrl);
            this.Controls.Add(this.pnlHeader);
            this.Name = "HPLoginProp";
            this.Size = new System.Drawing.Size(289, 146);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Base.PropInput piUrl;
        private Base.PropInput piUser;
        private Base.PropInput piPwd;
    }
}
