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

namespace JoJoSuite.Pdf
{
    public partial class PdfToText : UserControl
    {
        Control _nextControl;
        Control _prevControl;

        private string sPath;
        private string sModule;

        private string sCodeFolder;

        public PdfToText()
        {
            InitializeComponent();
            txtTitle.Text = this.Name;
        }

        public string Path
        {
            get
            {
                return sPath;
            }
            set
            {
                sPath = value;

                lblUrl.Text = "Path: " + sPath;

                Invalidate();
            }
        }

        public string Module
        {
            get
            {
                return sModule;
            }
            set
            {
                sModule = value;

                lblUser.Text = "Code Module: " + sModule;
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

                PdfToTextProp p1 = new PdfToTextProp();
                p1.PdfToText = this;
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

            if (File.Exists(sCodeFolder + @"\Pdf\PdfToText.txt"))
            {
                using (StreamReader reader = new StreamReader(sCodeFolder + @"\Pdf\PdfToText.txt"))
                {
                    res = reader.ReadToEnd();
                }
                res = res.Replace("{0}", sPath);
                res = res.Replace("{1}", sModule);
            }
            return res;
        }
    }
}
