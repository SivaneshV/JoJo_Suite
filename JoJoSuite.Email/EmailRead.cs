using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace JoJoSuite.Email
{
    public partial class EmailRead : UserControl
    {
        private string sMbox;
        private string sReadFromSubject;
        private string sFolder;
        private string sSession;

        private string sCodeFolder;


        Control _nextControl;
        Control _prevControl;

        public EmailRead()
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

                EmailReadProp p1 = new EmailReadProp();
                p1.EmailRead = this;
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

        public string Mbox
        {
            get
            {
                return sMbox;
            }
            set
            {
                sMbox = value;

                lblFolder.Text = "Mailbox: " + sMbox;

                Invalidate();
            }
        }

        public string ReadFromSubject
        {
            get
            {
                return sReadFromSubject;
            }
            set
            {
                sReadFromSubject = value;

                lblReadFromSubject.Text = "Subject Contains: " + sReadFromSubject;

                Invalidate();
            }
        }
        public string Folder
        {
            get
            {
                return sFolder;
            }
            set
            {
                sFolder = value;

                 lblFolder.Text= "Folder: " + sFolder;

                Invalidate();
            }
        }

        public string Session
        {
            get
            {
                return sSession;
            }
            set
            {
                sSession = value;

                lblSession.Text = "Session: " + sSession;

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

        private void EmailRead_Load(object sender, EventArgs e)
        {

        }

        public string GetCodeSnippet()
        {
            string res = "//CODE NOT AVAILABLE";

            if (File.Exists(sCodeFolder + @"\Email\EmailRead.txt"))
            {
                using (StreamReader reader = new StreamReader(sCodeFolder + @"\Email\EmailRead.txt"))
                {
                    res = reader.ReadToEnd();
                }



                res = res.Replace("{0}", sMbox);
                res = res.Replace("{1}", sReadFromSubject);
            }
            return res;
        }
    }
}
