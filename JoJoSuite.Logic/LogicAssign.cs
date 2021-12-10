using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using JoJoSuite.Web;
using JoJoSuite.Database;
using JoJoSuite.Pdf;
using JoJoSuite.Office.Excel;
using JoJoSuite.Email;

namespace JoJoSuite.Logic
{
    public partial class LogicAssign : UserControl
    {

        private string sTo;
        private string sValue;

        Control _nextControl;
        Control _prevControl;

        public LogicAssign()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            txtTitle.Text = this.Name.Replace("Logic", "");
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

                LogicAssignProp p1 = new LogicAssignProp();
                p1.LogicAssign = this;
                p1.Dock = DockStyle.Fill;
                tpProp.Controls.Clear();
                tpProp.Controls.Add(p1);
            }
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
            if (e.KeyCode == Keys.Delete)
            {
                this.DeleteControl(sender, e);
            }
        }

        //control properties
        //
        public string To
        {
            get
            {
                return sTo;
            }
            set
            {
                sTo = value;

                lblCondition.Text = sTo + " = " + sValue;

                Invalidate();
            }
        }

        public string Value
        {
            get
            {
                return sValue;
            }
            set
            {
                sValue = value;

                lblCondition.Text = sTo + " = " + sValue;

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

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void Control_Click(object sender, EventArgs e)
        {
            this.OnClick(new EventArgs());
        }
    }
}
