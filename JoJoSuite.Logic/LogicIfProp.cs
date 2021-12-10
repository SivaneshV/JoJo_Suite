using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Logic
{
    public partial class LogicIfProp : UserControl
    {
        private string sVal1;
        private int nOpt;
        private string sVal2;

        private LogicIf logicIf;
        
        public LogicIfProp()
        {
            InitializeComponent();
        }

        public string Value1
        {
            get
            {
                return sVal1;
            }
            set
            {
                sVal1 = value;
                Invalidate();
            }
        }

        public string Value2
        {
            get
            {
                return sVal2;
            }
            set
            {
                sVal2 = value;
                Invalidate();
            }
        }

        public int Operator
        {
            get
            {
                return nOpt;
            }
            set
            {
                nOpt = value;
                Invalidate();
            }
        }

        public LogicIf LogicIf
        {
            get
            {
                return logicIf;
            }
            set
            {
                logicIf = value;

                txtVal1.Text = sVal1 = value.Value1;
                txtVal2.Text = sVal2 = value.Value2;
                cbOpt.SelectedIndex = nOpt = value.Operator;

                Invalidate();
            }
        }

        private void txtVal2_TextChanged(object sender, EventArgs e)
        {
            LogicIf.Value2 = sVal2 = txtVal2.Text;
        }

        private void txtVal1_TextChanged(object sender, EventArgs e)
        {
            LogicIf.Value1 = sVal1 = txtVal1.Text;
        }

        private void cbOpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicIf.Operator = nOpt = cbOpt.SelectedIndex;
        }
    }
}
