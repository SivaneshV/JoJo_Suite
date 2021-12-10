using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace JoJoSuite.Email
{
    public partial class EmailLogin : UserControl
    {
        private string sExchange;
        private string sUser;
        private string sPwd;
        private string sDomain;

        private string sCodeFolder;

        Control _nextControl;
        Control _prevControl;

        public EmailLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblSmtpServer_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void txtTitle_Leave(object sender, EventArgs e)
        {
            pnlFirst.BackColor = SystemColors.ControlDark;
            pnlHeader.BackColor = txtTitle.BackColor = SystemColors.Control;
        }

        private void txtTitle_Enter(object sender, EventArgs e)
        {
            pnlFirst.BackColor = Color.DarkGoldenrod;
            pnlHeader.BackColor = txtTitle.BackColor = Color.Gold;
        }

        private void pnlMain_Click(object sender, EventArgs e)
        {
            txtTitle.Focus();

            Form frm1 = this.FindForm();

            Control[] ctrls = frm1.Controls.Find("tpProp", true);

            if (ctrls.Length == 1)
            {
                TabPage tpProp = (TabPage)ctrls[0];

                EmailLoginProp p1 = new EmailLoginProp();
                p1.EmailLogin = this;
                p1.Dock = DockStyle.Fill;
                tpProp.Controls.Clear();
                tpProp.Controls.Add(p1);
            }
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            //txtTitle.Focus();
            this.OnMouseDown(e);
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            //txtTitle.Focus();
            this.OnMouseMove(e);
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            //txtTitle.Focus();
            this.OnMouseUp(e);
        }

        public string Exchange
        {
            get
            {
                return sExchange;
            }
            set
            {
                sExchange = value;

                lblExUrl.Text = "Exchange: " + sExchange;

                Invalidate();
            }
        }

        public string User
        {
            get
            {
                return sUser;
            }
            set
            {
                sUser = value;

                lblUserName.Text = "User: " + sUser;

                Invalidate();
            }
        }

        public string Password
        {
            get
            {
                return sPwd;
            }
            set
            {
                sPwd = value;

                lblPwd.Text = "Password: " + sPwd;

                Invalidate();
            }
        }

        public string Domain
        {
            get
            {
                return sDomain;
            }
            set
            {
                sDomain = value;

                lblDomain.Text = "Domain: " + sDomain;

                Invalidate();
            }
        }

        public string CodeFolder
        {
            get
            {
                return sCodeFolder;
            }
            set
            {
                sCodeFolder = value;

                Invalidate();
            }
        }

        public Control NextControl
        {
            get
            {
                return _nextControl;
            }
            set
            {
                _nextControl = value;

                Invalidate();
            }
        }

        public Control PreviousControl
        {
            get
            {
                return _prevControl;
            }
            set
            {
                _prevControl = value;
                Invalidate();
            }
        }

        private void txtTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.DeleteControl(sender, e);
            }
        }

        public event KeyEventHandler DeleteControl;

        private void lblPwd_Click(object sender, EventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pnlFirst_Paint(object sender, PaintEventArgs e)
        {

        }

        public string GetCodeSnippet()
        {
            string res = "//CODE NOT AVAILABLE";

            if (File.Exists(sCodeFolder + @"\Email\EmailLogin.txt"))
            {
                using (StreamReader reader = new StreamReader(sCodeFolder + @"\Email\EmailLogin.txt"))
                {
                    res = reader.ReadToEnd();
                }

                res = res.Replace("{0}", sExchange);
                res = res.Replace("{1}", sUser);
                res = res.Replace("{2}", sPwd);
                res = res.Replace("{3}", sDomain);
            }
            return res;
        }
    }
}
