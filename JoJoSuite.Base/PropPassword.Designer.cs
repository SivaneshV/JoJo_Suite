namespace JoJoSuite.Base
{
    partial class PropPassword
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtVal = new System.Windows.Forms.TextBox();
            this.cboVar = new System.Windows.Forms.ComboBox();
            this.chkVal = new System.Windows.Forms.CheckBox();
            this.dtpVal = new System.Windows.Forms.DateTimePicker();
            this.btnCollection = new System.Windows.Forms.Button();
            this.chkVar = new System.Windows.Forms.CheckBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // txtVal
            // 
            this.txtVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVal.Location = new System.Drawing.Point(77, 2);
            this.txtVal.Name = "txtVal";
            this.txtVal.Size = new System.Drawing.Size(163, 20);
            this.txtVal.TabIndex = 1;
            this.txtVal.TextChanged += new System.EventHandler(this.txtVal_TextChanged);
            // 
            // cboVar
            // 
            this.cboVar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVar.FormattingEnabled = true;
            this.cboVar.Items.AddRange(new object[] {
            "Var1",
            "Var2",
            "Var3"});
            this.cboVar.Location = new System.Drawing.Point(77, 2);
            this.cboVar.Name = "cboVar";
            this.cboVar.Size = new System.Drawing.Size(163, 21);
            this.cboVar.TabIndex = 2;
            this.cboVar.Visible = false;
            this.cboVar.SelectedIndexChanged += new System.EventHandler(this.cboVar_SelectedIndexChanged);
            // 
            // chkVal
            // 
            this.chkVal.AutoSize = true;
            this.chkVal.Location = new System.Drawing.Point(77, 5);
            this.chkVal.Name = "chkVal";
            this.chkVal.Size = new System.Drawing.Size(15, 14);
            this.chkVal.TabIndex = 3;
            this.chkVal.UseVisualStyleBackColor = true;
            this.chkVal.Visible = false;
            this.chkVal.CheckedChanged += new System.EventHandler(this.chkVal_CheckedChanged);
            // 
            // dtpVal
            // 
            this.dtpVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpVal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVal.Location = new System.Drawing.Point(77, 2);
            this.dtpVal.Name = "dtpVal";
            this.dtpVal.Size = new System.Drawing.Size(163, 20);
            this.dtpVal.TabIndex = 4;
            this.dtpVal.Visible = false;
            this.dtpVal.ValueChanged += new System.EventHandler(this.dtpVal_ValueChanged);
            // 
            // btnCollection
            // 
            this.btnCollection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCollection.Location = new System.Drawing.Point(77, 2);
            this.btnCollection.Name = "btnCollection";
            this.btnCollection.Size = new System.Drawing.Size(163, 21);
            this.btnCollection.TabIndex = 7;
            this.btnCollection.Text = "Set Collection";
            this.btnCollection.UseVisualStyleBackColor = true;
            this.btnCollection.Visible = false;
            this.btnCollection.Click += new System.EventHandler(this.btnCollection_Click);
            // 
            // chkVar
            // 
            this.chkVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkVar.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkVar.FlatAppearance.BorderSize = 0;
            this.chkVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkVar.Image = global::JoJoSuite.Base.Properties.Resources.var01_16;
            this.chkVar.Location = new System.Drawing.Point(241, 0);
            this.chkVar.Name = "chkVar";
            this.chkVar.Size = new System.Drawing.Size(23, 23);
            this.chkVar.TabIndex = 6;
            this.chkVar.UseVisualStyleBackColor = true;
            this.chkVar.CheckedChanged += new System.EventHandler(this.chkVar_CheckedChanged);
            // 
            // txtPass
            // 
            this.txtPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPass.Location = new System.Drawing.Point(76, 2);
            this.txtPass.MaxLength = 250;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(163, 20);
            this.txtPass.TabIndex = 8;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.TextChanged += new System.EventHandler(this.txtPass_TextChanged);
            // 
            // PropPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.chkVar);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cboVar);
            this.Controls.Add(this.txtVal);
            this.Controls.Add(this.dtpVal);
            this.Controls.Add(this.chkVal);
            this.Controls.Add(this.btnCollection);
            this.Name = "PropPassword";
            this.Size = new System.Drawing.Size(268, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.ComboBox cboVar;
        private System.Windows.Forms.CheckBox chkVal;
        private System.Windows.Forms.DateTimePicker dtpVal;
        private System.Windows.Forms.CheckBox chkVar;
        private System.Windows.Forms.Button btnCollection;
        private System.Windows.Forms.TextBox txtPass;
    }
}
