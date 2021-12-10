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
    public partial class LogicStart : UserControl
    {

        Control _start;

        public LogicStart()
        {
            InitializeComponent();
        }

        //control properties
        //
        public Control NextControl
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;

                Invalidate();
            }
        }

        private void pnlMain_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("dragging control");

        }

        private void pnlMain_MouseHover(object sender, EventArgs e)
        {
            Console.WriteLine("dragging control");

        }
    }
}
