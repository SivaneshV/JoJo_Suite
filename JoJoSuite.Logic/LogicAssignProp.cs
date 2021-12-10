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
    public partial class LogicAssignProp : UserControl
    {
        private string sTo;
        private string sVal;

        private LogicAssign logicAssign;
        
        public LogicAssignProp()
        {
            InitializeComponent();
        }

        public string Value
        {
            get
            {
                return sVal;
            }
            set
            {
                sVal = value;
                Invalidate();
            }
        }

        public string To
        {
            get
            {
                return sTo;
            }
            set
            {
                sTo = value;
                Invalidate();
            }
        }

        public LogicAssign LogicAssign
        {
            get
            {
                return logicAssign;
            }
            set
            {
                logicAssign = value;

                piTo.Value = sTo = value.To;
                piValue.Value = sVal = value.Value;

                Invalidate();
            }
        }

        private void piValue_PropertyChanged(object sender, EventArgs e)
        {
            logicAssign.Value = sVal = piValue.Value;

        }

        private void piTo_PropertyChanged(object sender, EventArgs e)
        {
            logicAssign.To = sTo = piTo.Value;

        }
    }
}
