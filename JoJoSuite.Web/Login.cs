using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace JoJoSuite.Web
{
    public partial class Login : UserControl
    {
        private string sUrl;
        private string sUser;
        private string sPwd;

        private string sCodeFolder;

        Control _nextControl;
        Control _prevControl;

        public Login()
        {
            InitializeComponent();

            txtTitle.Text = this.Name;

         
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

                LoginProp p1 = new LoginProp();
                p1.HPLogin = this;
                p1.Dock = DockStyle.Fill;
                tpProp.Controls.Clear();
                tpProp.Controls.Add(p1);
            }
            //this.OnClick(new EventArgs());
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }

        private void txtTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                this.DeleteControl(sender, e);
            }
        }

        //control properties
        //
        public string Url
        {
            get
            {
                return sUrl;
            }
            set
            {
                sUrl = value;

                lblUrl.Text = "Url: " + sUrl;

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

                lblUser.Text = "User: " + sUser;

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

        //control events
        public event KeyEventHandler DeleteControl;


        //control methods
        public string GetCodeSnippet()
        {

            string res = "//CODE NOT AVAILABLE";

            if (File.Exists(sCodeFolder + @"\Email\EmailRead.txt"))
            {
                using (StreamReader reader = new StreamReader(sCodeFolder + @"\Email\EmailRead.txt"))
                {
                    res = reader.ReadToEnd();
                }
                res = res.Replace("{0}", Url);
                res = res.Replace("{1}", User);
                res = res.Replace("{2}", Password);
            }
            return res;
        }

        private void HPLogin_Load(object sender, EventArgs e)
        {
            Form frm1 = this.FindForm();

            Control[] ctrls = frm1.Controls.Find("lblLogo", true);

            if (ctrls.Length == 1)
            {
                Label lblLogo = (Label)ctrls[0];
                pnlMain.ContextMenuStrip = lblLogo.ContextMenuStrip;
            }
        }
    }
}
