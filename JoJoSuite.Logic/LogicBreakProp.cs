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
    public partial class LogicBreakProp : UserControl
    {
        private string sLoop;

        private LogicBreak logicBreak;
        
        public LogicBreakProp()
        {
            InitializeComponent();
        }

        public string Loop
        {
            get
            {
                return sLoop;
            }
            set
            {
                sLoop = value;
                Invalidate();
            }
        }

        public LogicBreak LogicBreak
        {
            get
            {
                return logicBreak;
            }
            set
            {
                logicBreak = value;
                Invalidate();
            }
        }

        private void piLoop_PropertyChanged(object sender, EventArgs e)
        {
        }
    }
}
