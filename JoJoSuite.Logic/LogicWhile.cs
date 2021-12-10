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
    public partial class LogicWhile : UserControl
    {

        private string sValue1;
        private int nOperator;
        private string sValue2;

        Control _nextControl;
        Control _prevControl;

        //public enum LogicOperator { Equal, NotEqual, GreaterThan, LessThan, GreaterThanEqual, LessThanEqual };

        public LogicWhile()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            txtTitle.Text = this.Name.Replace("Logic", "");
        }

        private void txtTitle_Leave(object sender, EventArgs e)
        {
            pnlFirst.BackColor = SystemColors.ControlDark;
            pnlHeader.BackColor = txtTitle.BackColor = lblWhile.BackColor = SystemColors.Control;
            pnlWhile.BackColor = SystemColors.ControlDark;
        }

        private void txtTitle_Enter(object sender, EventArgs e)
        {
            pnlFirst.BackColor = pnlWhile.BackColor = Color.DarkGoldenrod;
            pnlHeader.BackColor = txtTitle.BackColor = lblWhile.BackColor = Color.Gold;
        }

        private void pnlMain_Click(object sender, EventArgs e)
        {
            txtTitle.Focus();

            Form frm1 = this.FindForm();

            Control[] ctrls = frm1.Controls.Find("tpProp", true);

            if (ctrls.Length == 1)
            {
                TabPage tpProp = (TabPage)ctrls[0];

                LogicWhileProp p1 = new LogicWhileProp();
                p1.LogicWhile = this;
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
        public string Value1
        {
            get
            {
                return sValue1;
            }
            set
            {
                sValue1 = value;

                lblCondition.Text = sValue1;

                if (nOperator == 0)
                {
                    lblCondition.Text += " = ";
                }
                else if (nOperator == 1)
                {
                    lblCondition.Text += " != ";
                }
                else if (nOperator == 2)
                {
                    lblCondition.Text += " > ";
                }
                else if (nOperator == 3)
                {
                    lblCondition.Text += " < ";
                }
                else if (nOperator == 4)
                {
                    lblCondition.Text += " >= ";
                }
                else if (nOperator == 5)
                {
                    lblCondition.Text += " <= ";
                }

                lblCondition.Text += sValue2;

                Invalidate();
            }
        }

        public string Value2
        {
            get
            {
                return sValue2;
            }
            set
            {
                sValue2 = value;

                lblCondition.Text = sValue1;

                if (nOperator == 0)
                {
                    lblCondition.Text += " = ";
                }
                else if (nOperator == 1)
                {
                    lblCondition.Text += " != ";
                }
                else if (nOperator == 2)
                {
                    lblCondition.Text += " > ";
                }
                else if (nOperator == 3)
                {
                    lblCondition.Text += " < ";
                }
                else if (nOperator == 4)
                {
                    lblCondition.Text += " >= ";
                }
                else if (nOperator == 5)
                {
                    lblCondition.Text += " <= ";
                }

                lblCondition.Text += sValue2;

                Invalidate();
            }
        }

        public int Operator
        {
            get
            {
                return nOperator;
            }
            set
            {
                nOperator = value;

                lblCondition.Text = sValue1;

                if (nOperator == 0)
                {
                    lblCondition.Text += " = ";
                }
                else if (nOperator == 1)
                {
                    lblCondition.Text += " != ";
                }
                else if (nOperator == 2)
                {
                    lblCondition.Text += " > ";
                }
                else if (nOperator == 3)
                {
                    lblCondition.Text += " < ";
                }
                else if (nOperator == 4)
                {
                    lblCondition.Text += " >= ";
                }
                else if (nOperator == 5)
                {
                    lblCondition.Text += " <= ";
                }

                lblCondition.Text += sValue2;

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

        private void Control_DeleteControl(object sender, KeyEventArgs e)
        {
            Control ctrl = (Control)sender;
            Control parent = ctrl.Parent;

            bool endless = true;

            while (endless)
            {
                //System.Windows.Forms.Form


                if (parent.Name == "pnlWhileMain")
                {
                    parent.Controls.Remove(ctrl);
                    endless = false;
                }
                else
                {
                    ctrl = parent;
                    parent = parent.Parent;
                }
            }

            AdjustHeight(this, (Panel)parent);
        }

        private void pnlWhileMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pnlWhileMain_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            string selNode = e.Data.GetData("System.String").ToString();
            AddControl(pnlWhileMain, selNode);

            //this.OnDragDrop(e);
        }

        private void AddControl(Panel pnl, string selNode)
        {

            if (selNode.ToString() == "nodeWebHPLogin")
            {
                Login c1 = new Login();
                //c1.Location = loc;
                c1.Click += new EventHandler(Control_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }
            else if (selNode.ToString() == "nodeLogicIf")
            {
                LogicIf c1 = new LogicIf();
                //c1.Location = loc;
                c1.Click += new EventHandler(Control_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }
            else if (selNode.ToString() == "nodeLogicWhile")
            {
                LogicWhile c1 = new LogicWhile();
                //c1.Location = loc;
                c1.Click += new EventHandler(Control_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }
            else if (selNode.ToString() == "nodeDatabaseConnect")
            {
                ConnectToDb c1 = new ConnectToDb();
                //c1.Location = loc;
                c1.Click += new EventHandler(Control_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }

            else if (selNode.ToString() == "nodePdfToText")
            {
                PdfToText c1 = new PdfToText();
                //c1.Location = loc;
                //c1.Click += new EventHandler(Web_HPLogin_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }

            else if (selNode.ToString() == "nodePdfToHtml")
            {
                PdfToHtml c1 = new PdfToHtml();
                //c1.Location = loc;
                //c1.Click += new EventHandler(Web_HPLogin_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }

            else if (selNode.ToString() == "nodeExcelOpenWorkbook")
            {
                OpenWorkbook c1 = new OpenWorkbook();
                //c1.Location = loc;
                //c1.Click += new EventHandler(Web_HPLogin_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }

            else if (selNode.ToString() == "nodeExcelCreateWorkbook")
            {
                CreateWorkbook c1 = new CreateWorkbook();
                //c1.Location = loc;
                //c1.Click += new EventHandler(Web_HPLogin_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }

            else if (selNode.ToString() == "nodeEmailLogin")
            {
                EmailLogin c1 = new EmailLogin();
                //c1.Location = loc;
                //c1.Click += new EventHandler(Web_HPLogin_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }

            else if (selNode.ToString() == "nodeEmailGetEmails")
            {
                EmailRead c1 = new EmailRead();
                //c1.Location = loc;
                //c1.Click += new EventHandler(Web_HPLogin_Click);
                //c1.MouseDown += new MouseEventHandler(Control_MouseDown);
                //c1.MouseMove += new MouseEventHandler(Control_MouseMove);
                //c1.MouseUp += new MouseEventHandler(Control_MouseUp);
                c1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                pnl.Controls.Add(c1);
            }

            AdjustWidth(this);

            AdjustHeight(this, pnl);
        }

        private void AdjustHeight(LogicWhile parent, Panel pnl)
        {
            int hightTrue = 0;

            foreach (Control c1 in parent.pnlWhileMain.Controls)
            {
                hightTrue += c1.Height + 10;
            }

            parent.Height = hightTrue + 150;

            Point loc = new Point(10, 10);

            foreach (Control c1 in pnl.Controls)
            {
                c1.Location = loc;
                loc.Y = c1.Location.Y + c1.Height + 10;
            }

            if (parent.Parent.Name == "pnlWhileMain")
            {
                AdjustHeight((LogicWhile)parent.Parent.Parent.Parent.Parent.Parent.Parent, (Panel)parent.Parent);
            }

        }

        private void AdjustWidth(LogicWhile parent)
        {

            int maxWidth = 0;

            foreach(Control c2 in parent.pnlWhileMain.Controls)
            {
                int maxWidth2 = c2.Width;

                if (maxWidth2 > maxWidth)
                {
                    maxWidth = maxWidth2;
                }
            }

            pnlWhileMain.Width = maxWidth + 25;

            if (parent.Width < (maxWidth + 50))
            {
                parent.Width = maxWidth + 50;
            }

            if (parent.Parent.Name == "pnlWhileMain")
            {
                AdjustWidth((LogicWhile)parent.Parent.Parent.Parent.Parent.Parent.Parent);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void Control_Click(object sender, EventArgs e)
        {
            this.OnClick(new EventArgs());
        }
    }
}
