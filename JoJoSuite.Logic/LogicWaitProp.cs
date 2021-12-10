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
    public partial class LogicWaitProp : UserControl
    {
        private int _duration;

        private LogicWait logicWait;
        
        public LogicWaitProp()
        {
            InitializeComponent();
        }

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                Invalidate();
            }
        }

        public LogicWait LogicWait
        {
            get
            {
                return logicWait;
            }
            set
            {
                logicWait = value;

                piDuration.Value = value.Duration.ToString();

                Invalidate();
            }
        }

        private void piDuration_PropertyChanged(object sender, EventArgs e)
        {
            logicWait.Duration = _duration = Convert.ToInt32(piDuration.Value);
        }
    }
}
