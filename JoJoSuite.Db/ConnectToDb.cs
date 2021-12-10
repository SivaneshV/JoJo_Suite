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
using System.Data.SqlClient;
namespace JoJoSuite.Database
{
    public partial class ConnectToDb : UserControl
    {
        private string sServer;
        private string sDb;
        private string sUser;
        private string sPwd;

        private string sCodeFolder;


        Control _nextControl;
        Control _prevControl;

        public ConnectToDb()
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

                ConnectToDbProp p1 = new ConnectToDbProp();
                p1.ConnectToDb = this;
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

        private void txtTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.DeleteControl(sender, e);
            }
        }

        //control properties
        //
        public string Server
        {
            get
            {
                return sServer;
            }
            set
            {
                sServer = value;

                lblServer.Text = "Server: " + sServer;

                Invalidate();
            }
        }

        public string Database
        {
            get
            {
                return sDb;
            }
            set
            {
                sDb = value;

                lblServer.Text = "Database: " + sDb;

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

                lblDb.Text = "User: " + sUser;

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

            if (System.IO.File.Exists(sCodeFolder + @"\Db\ConnectToDb.txt"))
            {
                using (StreamReader reader = new StreamReader(sCodeFolder + @"\Db\ConnectToDb.txt"))
                {
                    res = reader.ReadToEnd();
                }
                res = res.Replace("{0}", sServer);
                res = res.Replace("{1}", sDb);
                res = res.Replace("{2}", sUser);
                res = res.Replace("{3}", sPwd);
            }
            return res;
        }
    }
}
