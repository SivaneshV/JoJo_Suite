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

namespace JoJoSuite.Web
{
    public partial class GetText : UserControl
    {
        private string sType;
        private string sText;
        

        Control _nextControl;
        Control _prevControl;

        public GetText()
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

                GetTextProp p1 = new GetTextProp();
                p1.GetText = this;
                p1.Dock = DockStyle.Fill;
                tpProp.Controls.Clear();
                tpProp.Controls.Add(p1);
            }
            //this.OnClick(new EventArgs());
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
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                this.DeleteControl(sender, e);
            }
        }

        //control properties
        //
        public string Type
        {
            get
            {
                return sType;
            }
            set
            {
                sType = value;

                lblType.Text = "Type: " + sType;

                Invalidate();
            }
        }

        public string Text
        {
            get
            {
                return sText;
            }
            set
            {
                sText = value;

                lblText.Text = "Text: " + sText;

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
            string res = "";
            using (StreamReader reader = new StreamReader("code002.txt"))
            {
                res = reader.ReadToEnd();
            }
            res = res.Replace("{0}", sType);
            res = res.Replace("{1}", sText);

            return res;
        }

        //static IWebElement getSingle(ChromeDriver parent, string sPath)
        //{
        //    IWebElement res = null;
        //    try
        //    {
        //        res = parent.FindElement(By.XPath(sPath));
        //    }
        //    catch (Exception)
        //    {
        //        //throw;
        //    }
        //    return res;
        //}
    }
}
